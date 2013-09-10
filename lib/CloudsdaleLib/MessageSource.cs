using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;
using CloudsdaleWin7.lib.Helpers;

namespace CloudsdaleWin7.lib.CloudsdaleLib {
    public class MessageSource : CloudsdaleModel{
        private static readonly Dictionary<string, MessageSource> Sources = new Dictionary<string, MessageSource>(); 
        public readonly ObservableCollection<Message> Messages = new ObservableCollection<Message>();
        private readonly Dictionary<string, Status> userStatuses = new Dictionary<string, Status>();
        private static Cloud _cloud { get; set; }
        public Cloud Source
        {
            get { return _cloud; }
        }
        private int _unreadMessages;

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

        public static MessageSource GetSource(Cloud cloud) {
            if (Sources.ContainsKey(cloud.Id)) {
                return Sources[cloud.Id];
            }
            _cloud = cloud;
            return Sources[cloud.Id] = new MessageSource();
            
        }
        public static MessageSource GetSource(string cloudId) {
            if (Sources.ContainsKey(cloudId)) {
                return Sources[cloudId];
            }
            return Sources[cloudId] = new MessageSource();
        }

        public void AddMessage(JToken message) {
            if (MainWindow.Instance.Dispatcher.CheckAccess()) {
                Messages.Add(message.ToObject<Message>());
            } else {
                MainWindow.Instance.Dispatcher.BeginInvoke(new Action(() => Messages.Add(message.ToObject<Message>())));
            }
        }
        #region Message Process
        public void ProcessMessage(JToken message)
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
            ++UnreadMessages;
            if (App.Connection.MessageController.CurrentCloud.CloudMessages == this)
            {
                UnreadMessages = 0;
            }
            App.Connection.MessageController.UpdateUnread();
            var message = UpdateSource(this, jMessage);
            //show notification

            message.Author.CopyTo(message.User);
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
        private static void OnCloudData(JToken cloudData)
        {
            cloudData.ToObject<Cloud>().CopyTo(_cloud);
        }
        private void SetStatus(string userId, Status status)
        {
            userStatuses[userId] = status;
            OnPropertyChanged("OnlineModerators");
            OnPropertyChanged("AllModerators");
            OnPropertyChanged("OnlineUsers");
            OnPropertyChanged("AllUsers");
        }
        private Message UpdateSource(MessageSource source, JToken message)
        {
            lock (source)
            {
                Message lastMsg;
                if (Messages.Any()
                    && (lastMsg = source.Messages.Last()).AuthorId == (string)message["author"]["id"]
                    && !lastMsg.Content.StartsWith("/me")
                    && !message["content"].ToString().StartsWith("/me"))
                {
                    lastMsg.Content += "\n" + message["content"];
                    var oldDrops = lastMsg.AllDrops.ToList();
                    foreach (var drop in message["drops"])
                    {
                        oldDrops.Add(drop.ToObject<Drop>());
                    }
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        Messages.RemoveAt(source.Messages.Count - 1);
                        return (lastMsg);
                    });
                }
                else
                {
                    message["content"] = message["content"]
                        .ToString().RegexReplace("^/me", (string)message["author"]["name"]);
                }
            }
            return (message.ToObject<Message>());
        }
        #endregion
    }
}
