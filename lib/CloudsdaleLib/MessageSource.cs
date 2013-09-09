using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib {
    public class MessageSource : CloudsdaleModel{
        private static readonly Dictionary<string, MessageSource> Sources = new Dictionary<string, MessageSource>(); 
        public readonly ObservableCollection<Message> Messages = new ObservableCollection<Message>();
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

        public MessageSource()
        {
            Messages.CollectionChanged += OnMessageReceive;
        }

        public static MessageSource GetSource(Cloud cloud) {
            if (Sources.ContainsKey(cloud.Id)) {
                return Sources[cloud.Id];
            }
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
        public void OnMessageReceive(object sender, EventArgs e)
        {
            UnreadMessages += 1;
            Console.WriteLine(UnreadMessages);
        }
    }
}
