using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class MessageController :  ICloudServicesProvider, INotifyPropertyChanged
    {
        public Dictionary<string, CloudView> Pages { get; set; } 
        public Dictionary<string, CloudController> CloudControllers = new Dictionary<string, CloudController>();
        public Dictionary<CloudController, List<Message>> CloudMessages { get; set; } 
        public CloudController CurrentCloud { get; set; }

        public MessageController()
        {
            CloudMessages = new Dictionary<CloudController, List<Message>>();
        }

        public async Task<CloudView> Page(Cloud cloud)
        {
            if (Pages.ContainsKey(cloud.Id)) return Pages[cloud.Id];
            await this[cloud].EnsureLoaded();
            return new CloudView(cloud);
        }

        public void OnMessage(JObject message)
        {
            MainWindow.Instance.MainFrame.Dispatcher.Invoke(()=>InternalOnMessage(message));
        }

        private void InternalOnMessage(JObject message)
        {
            var chanSplit = ((string)message["channel"]).Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (chanSplit.Length < 2) return;

            switch (chanSplit[0])
            {
                case "clouds":
                    if (CloudControllers.ContainsKey(chanSplit[1]))
                    {
                        CloudControllers[chanSplit[1]].OnMessage(message);
                        break;
                    }
                    CloudControllers.Add(chanSplit[1], new CloudController(App.Connection.ModelController.GetCloud(chanSplit[1])));
                    break;
                case "users":
                    var sessionController = App.Connection.SessionController;
                    if (sessionController.CurrentSession == null) break;
                    if (chanSplit[1] == sessionController.CurrentSession.Id)
                    {
                        App.Connection.SessionController.OnMessage(message);
                    }
                    break;
            }
        }

        public CloudController this[Cloud cloud]
        {
            get
            {
                if (!CloudControllers.ContainsKey(cloud.Id))
                {
                    CloudControllers[cloud.Id] = new CloudController(cloud);
                }

                return CloudControllers[cloud.Id];
            }
        }

        internal void UpdateUnread()
        {
            OnPropertyChanged("TotalUnreadMessages");
        }

        public int TotalUnreadMessages
        {
            get
            {
                return CloudControllers.Select(controller => 
                    controller.Value.UnreadMessages)
                                       .Aggregate(0, (total, i) => total + i);
            }
        }

        public IStatusProvider StatusProvider(string cloudId)
        {
            return CloudControllers[cloudId];
        }

        public User GetBackedUser(string userId)
        {
            return App.Connection.ModelController.GetUser(userId);
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
