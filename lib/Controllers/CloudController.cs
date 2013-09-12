using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.Cloud;
using CloudsdaleWin7.lib.CloudsdaleLib;
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
        private static Cloud cloud;
        private readonly Dictionary<string, Status> userStatuses = new Dictionary<string, Status>();
        private DateTime? _validatedFayeClient;
        private readonly UserList _userList;
        public MessageSource CloudMessages;

        public void ShowUserList()
        {
            Main.Instance.FlyoutFrame.Navigate(new UserList(this));
        }

        public CloudController(Cloud cloud)
        {
            Cloud = cloud;
            CloudMessages = MessageSource.GetSource(cloud);
        }

        public Cloud Cloud { get; private set; }

        public List<User> OnlineModerators
        {
            get
            {
                var list =
                    userStatuses.Where(kvp => kvp.Value != Status.Offline)
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
                    userStatuses.Where(kvp => kvp.Value != Status.Offline)
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
                    userStatuses.Where(kvp => !Cloud.ModeratorIds.Contains(kvp.Key))
                                .Select(kvp => App.Connection.ModelController.GetUser(kvp.Key))
                                .ToList();
                list.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
                return list;
            }
        }

        public async Task EnsureLoaded()
        {
            FayeConnector.Subscribe("/clouds/" + Cloud.Id + "/users/*");

            await Cloud.Validate();

            var client = new HttpClient().AcceptsJson();

            // Load user list
            {
                var response = await client.GetStringAsync((
                    Cloud.UserIds.Length > 100
                    ? Endpoints.CloudOnlineUsers
                    : Endpoints.CloudUsers)
                    .Replace("[:id]", Cloud.Id));
                var userData = JsonConvert.DeserializeObject<WebResponse<User[]>>(response);
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
            userStatuses[userId] = status;
            OnPropertyChanged("OnlineModerators");
            OnPropertyChanged("AllModerators");
            OnPropertyChanged("OnlineUsers");
            OnPropertyChanged("AllUsers");
            return status;
        }

        public Status StatusForUser(string userId)
        {
            return userStatuses.ContainsKey(userId) ? userStatuses[userId] : SetStatus(userId, Status.Offline);
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
