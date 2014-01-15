using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class CloudController : IStatusProvider, INotifyPropertyChanged
    {
        private int _unreadMessages;
        private readonly Dictionary<string, Status> _userStatuses = new Dictionary<string, Status>();
        private readonly ObservableCollection<Ban> _bans = new ObservableCollection<Ban>();
        public static readonly Regex LinkRegex = new Regex(@"(?i)\b((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", RegexOptions.IgnoreCase);
        public MessageSource Source { get; set; }

        public User Owner
        {
            get { return GetOwner(); }
        }

        private User GetOwner()
        {
            var client = new HttpClient().AcceptsJson();
            return
                JsonConvert.DeserializeObjectAsync<WebResponse<User>>(client.GetStringAsync(Endpoints.User.Replace("[:id]", Cloud.OwnerId)).Result).
                    Result.Result;
        }

        public CloudController(Cloud cloud)
        {
            Cloud = cloud;
            FixSessionStatus();
            Source = MessageSource.GetSource(cloud.Id);
            //LoadBans();
        }


        public Cloud Cloud { get; private set; }

        public ObservableCollection<Ban> Bans
        {
            get { return _bans; }
        }

        public List<User> OnlineModerators
        {
            get
            {
                var list =
                    _userStatuses.Where(kvp => kvp.Value != Status.Offline)
                                .Where(kvp => Cloud.ModeratorIds.Contains(kvp.Key))
                                .Select(kvp => App.Connection.ModelController.GetUser(kvp.Key))
                                .ToList();
                list.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
                return list;
            }
        }


        public List<User> AllModerators
        {
            get
            {
                var list =
                    Cloud.ModeratorIds
                                .Select(mid => App.Connection.ModelController.GetUser(mid))
                                .ToList();
                list.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
                return list;
            }
        }


        public List<User> OnlineUsers
        {
            get
            {
                var list =
                    _userStatuses.Where(kvp => kvp.Value != Status.Offline)
                                .Where(kvp => !Cloud.ModeratorIds.Contains(kvp.Key))
                                .Select(kvp => App.Connection.ModelController.GetUser(kvp.Key))
                                .ToList();
                list.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
                return list;
            }
        }
        public List<User> AllUsers
        {
            get
            {
                var list =
                    _userStatuses.Where(kvp => !Cloud.ModeratorIds.Contains(kvp.Key))
                                .Select(kvp => App.Connection.ModelController.GetUser(kvp.Key))
                                .ToList();
                list.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
                return list;
            }
        }

        public async Task LoadUsers()
        {
            try
            {
                var client = new HttpClient().AcceptsJson();
                {
                    var response = await client.GetStringAsync((
                        Cloud.UserIds.Length > 100
                        ? Endpoints.CloudOnlineUsers
                        : Endpoints.CloudUsers)
                        .Replace("[:id]", Cloud.Id));
                    var userData = await JsonConvert.DeserializeObjectAsync<WebResponse<User[]>>(response);
                    var users = new List<User>();
                    foreach (var user in userData.Result)
                    {
                        if (user.Status != null)
                        {
                            SetStatus(user.Id, (Status)user.Status);
                        }
                        users.Add(await App.Connection.ModelController.UpdateDataAsync(user));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task LoadBans()
        {
            var client = new HttpClient().AcceptsJson();
            client.DefaultRequestHeaders.Add("X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken);

            if (!Cloud.ModeratorIds.Contains(App.Connection.SessionController.CurrentSession.Id)) return;
            _bans.Clear();
            var response = await client.GetStringAsync(Endpoints.CloudBan.Replace("[:id]", Cloud.Id));
            var userData = await JsonConvert.DeserializeObjectAsync<WebResponse<Ban[]>>(response);
            foreach (var ban in userData.Result)
            {
                var b = ban;
                foreach (var user in App.Connection.ModelController.Users.Where(user => b.OffenderId == user.Key))
                {
                    user.Value.BansOnUser.Add(ban);
                }
            }
        }

        public async Task LoadBans(string id)
        {
            var client = new HttpClient().AcceptsJson();
            client.DefaultRequestHeaders.Add("X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken);

            if (!Cloud.ModeratorIds.Contains(App.Connection.SessionController.CurrentSession.Id)) return;
            _bans.Clear();
            var response = await client.GetStringAsync(Endpoints.CloudBan.Replace("[:id]", Cloud.Id));
            var userData = await JsonConvert.DeserializeObjectAsync<WebResponse<Ban[]>>(response);
            foreach (var ban in userData.Result.Where(b => b.Expired == false && b.Revoked == false && b.OffenderId == id))
            {
                App.Connection.ModelController.Users[id].BansOnUser.Add(ban);
            }
        }

        public async Task EnsureLoaded()
        {
            FayeConnector.Subscribe("/clouds/" + Cloud.Id + "/users/**");

            await Cloud.Validate();
            await LoadMessages();
            await LoadUsers();
            await LoadBans();
        }

        public async Task LoadMessages(bool addUnread = true)
        {
            var client = new HttpClient().AcceptsJson();

            var response = await client.GetStringAsync(Endpoints.CloudMessages.Replace("[:id]", Cloud.Id));
            var responseMessages = await JsonConvert.DeserializeObjectAsync<WebResponse<Message[]>>(response);
            var newMessages = new List<Message>(MessageSource.GetSource(Cloud.Id).Messages
                .Where(message => message.Timestamp > responseMessages.Result.Last().Timestamp));
            MessageSource.GetSource(Cloud.Id).Messages.Clear();
            try
            {
                foreach (var message in responseMessages.Result)
                {
                    StatusForUser(message.Author.Id);
                    AddMessageToSource(message, addUnread);
                }
                foreach (var message in newMessages)
                {
                    StatusForUser(message.Author.Id);
                    AddMessageToSource(message, addUnread);
                }
            }
            catch (Exception e)
            {
                App.Connection.NotificationController.Notification.Notify(e.Message);
                //LoadMessages();
            }
        }


        public void AddMessageToSource(Message message, bool addUnread = true)
        {
            if (!App.Connection.ModelController.Users.ContainsKey(message.Author.Id))
                App.Connection.ModelController.Users.Add(message.Author.Id, message.Author);
            message.Author.CopyTo(message.User);
            message.Timestamp = message.Timestamp.ToLocalTime();

            if (Source.Messages.Count > 0)
            {
                if (Source.Messages.Last().AuthorId == message.AuthorId
                    && !message.Content.StartsWith("/me")
                    && !MessageSource.GetSource(Cloud.Id).Messages.Last().Content.StartsWith("/me"))
                {
                    Source.Messages[Source.Messages.Count - 1].Content += "\n" + message.Content;
                    if (addUnread) AddUnread();
                }
                else Source.AddMessages(message);
            }
            else Source.AddMessages(message);

            MainWindow.Instance.Title = String.Format("{0} ({1} Unread Messages)", ((Page)Main.Instance.Frame.Content).Title, App.Connection.MessageController.TotalUnreadMessages);
            if (Source.Messages.Count > 50) Source.Messages.RemoveAt(0);
        }

        public void OnMessage(JObject message)
        {
            var chanSplit = ((string)message["channel"]).Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (chanSplit.Length == 4 && chanSplit[2] == "chat" && chanSplit[3] == "messages")
            {
                OnChatMessage(message["data"]);
            }
            else if (chanSplit.Length == 4 && chanSplit[2] == "users")
            {
                OnUserMessage(chanSplit[3], message["data"]);
            }
            else if (chanSplit.Length == 2)
            {
                OnCloudData(message["data"]);
            }
        }

        private void OnChatMessage(JToken jMessage)
        {
            AddUnread();
            var message = jMessage.ToObject<Message>();
            message.Author.CopyTo(message.User);

            message.PostedOn = Cloud.Id;

            AddMessageToSource(message);

            #region Cloud Note


            if (App.Connection.NotificationController.Receive)
            {
                if (Cloud.IsSubscribed)
                {
                    App.Connection.NotificationController.Notification.Notify(NotificationType.Cloud, message);
                }
            }


            #endregion


            if (App.Connection.MessageController.CurrentCloud == this && CloudView.Instance != null && !CloudView.Instance.IsReadingHistory)
            {
                CloudView.Instance.ChatScroll.ScrollToBottom();
            }
        }

        private async void OnUserMessage(string id, JToken jUser)
        {
            jUser["id"] = id;
            var user = jUser.ToObject<User>();
            if (user.Status != null)
            {
                SetStatus(user.Id, (Status)user.Status);
            }
            try
            {
                await App.Connection.ModelController.UpdateDataAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnCloudData(JToken cloudData)
        {
            cloudData.ToObject<Cloud>().CopyTo(Cloud);
        }

        private void AddUnread()
        {
            ++UnreadMessages;
            if (App.Connection.MessageController.CurrentCloud == this && App.Connection.MessageController.HasCloudSelected)
            {
                UnreadMessages = 0;
            }
            App.Connection.MessageController.UpdateUnread();
        }

        public int UnreadMessages
        {
            get { return _unreadMessages; }
            set
            {
                if (value == _unreadMessages) return;
                _unreadMessages = value;
                OnPropertyChanged();
            }
        }

        private Status SetStatus(string userId, Status status)
        {
            FixSessionStatus();
            _userStatuses[userId] = status;
            OnPropertyChanged("OnlineModerators");
            OnPropertyChanged("AllModerators");
            OnPropertyChanged("OnlineUsers");
            OnPropertyChanged("AllUsers");
            return status;
        }


        public Status StatusForUser(string userId)
        {
            FixSessionStatus();
            return _userStatuses.ContainsKey(userId) ? _userStatuses[userId] : SetStatus(userId, Status.Offline);
        }


        private void FixSessionStatus()
        {
            _userStatuses[App.Connection.SessionController.CurrentSession.Id] =
                App.Connection.SessionController.CurrentSession.PreferredStatus;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
