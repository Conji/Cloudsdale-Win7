using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class MessageController :  ICloudServicesProvider, INotifyPropertyChanged
    {
        private readonly Dictionary<string, CloudController> _cloudControllers = new Dictionary<string, CloudController>();
        public CloudController CurrentCloud { get; set; }
        private readonly SessionController _sessionController = new SessionController();
        private readonly ModelController _modelController = new ModelController();

        public void OnMessage(JObject message)
        {
            InternalOnMessage(message);
        }

        private void InternalOnMessage(JObject message)
        {
            var chanSplit = ((string)message["channel"]).Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (chanSplit.Length < 2) return;

            switch (chanSplit[0])
            {
                case "clouds":
                    if (_cloudControllers.ContainsKey(chanSplit[1]))
                    {
                        _cloudControllers[chanSplit[1]].OnMessage(message);
                    }
                    break;
                case "users":
                    _sessionController.OnMessage(message);
                    break;
            }
        }

        public CloudController this[Cloud cloud]
        {
            get
            {
                if (!_cloudControllers.ContainsKey(cloud.Id))
                {
                    _cloudControllers[cloud.Id] = new CloudController(cloud);
                }

                return _cloudControllers[cloud.Id];
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
                return _cloudControllers.Select(controller => controller.Value.UnreadMessages)
                                       .Aggregate(0, (total, i) => total + i);
            }
        }

        public IStatusProvider StatusProvider(string cloudId)
        {
            return _cloudControllers[cloudId];
        }

        public User GetBackedUser(string userId)
        {
            return _modelController.GetUser(userId);
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
