using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class CloudController : IStatusProvider,  INotifyPropertyChanged
    {
        private int _unreadMessages;
        private readonly Dictionary<string, Status> _userStatuses = new Dictionary<string, Status>();
        private readonly List<Message> _messages = new List<Message>();
        private readonly ObservableCollection<Ban> _bans = new ObservableCollection<Ban>();

        public CloudController(Cloud cloud)
        {
            Cloud = cloud;
            FixSessionStatus();
        }

        public Cloud Cloud { get; private set; }

        public List<Message> Messages { get { return _messages; } }

        public ObservableCollection<Ban> Bans
        {
            get 
            { 
                LoadBans();
                return _bans;
            }
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LoadBans()
        {
            var client = new HttpClient().AcceptsJson();
            {
                _bans.Clear();
                var response = await client.GetStringAsync(Endpoints.CloudBan.Replace("[:id]", Cloud.Id));
                var userData = await JsonConvert.DeserializeObjectAsync<WebResponse<Ban[]>>(response);
                foreach (var ban in userData.Result)
                {
                    if (ban.Revoked == true) return;
                    if (ban.Active == false) return;
                    if (ban.Expired == true) return;
                    _bans.Add(ban);
                }
            }
        }

        public async Task EnsureLoaded()
        {
            FayeConnector.Subscribe("/clouds/" + Cloud.Id + "/users/*");

            Cloud.Validate();

            var client = new HttpClient().AcceptsJson();

            // Load user list
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

            // Load messages
            {
                var response = await client.GetStringAsync(Endpoints.CloudMessages.Replace("[:id]", Cloud.Id));
                var responseMessages = await JsonConvert.DeserializeObjectAsync<WebResponse<Message[]>>(response);
                var newMessages = new List<Message>(_messages
                    .Where(message => message.Timestamp > responseMessages.Result.Last().Timestamp));
                _messages.Clear();
                foreach (var message in responseMessages.Result)
                {
                    StatusForUser(message.Author.Id);
                    _messages.Add(message);
                }
                foreach (var message in newMessages)
                {
                    StatusForUser(message.Author.Id);
                    _messages.Add(message);
                    if (_messages.Count <= 50) return;
                    _messages.RemoveAt(50);
                }
            }

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

            if (message.ClientId == FayeConnector.ClientID) return;

            

            message.Author.CopyTo(message.User);
            _messages.Add(message);
            if (_messages.Count <= 50) return;
            _messages.RemoveAt(50);
        }

        
        private async void OnUserMessage(string id, JToken jUser)
        {
            jUser["id"] = id;
            var user = jUser.ToObject<User>();
            if (user.Status != null)
            {
                SetStatus(user.Id, (Status)user.Status);
            }
            await App.Connection.ModelController.UpdateDataAsync(user);
        }

        private void OnCloudData(JToken cloudData)
        {
            cloudData.ToObject<Cloud>().CopyTo(Cloud);
        }

        private void AddUnread()
        {
            ++UnreadMessages;
            if (App.Connection.MessageController.CurrentCloud == this)
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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
