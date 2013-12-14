using System.Collections.Generic;
using System.Collections.ObjectModel;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.lib.CloudsdaleLib
{
    public class MessageSource
    {
        private static readonly Dictionary<string, MessageSource> Sources = new Dictionary<string, MessageSource>();
        public readonly ObservableCollection<Message> Messages = new ObservableCollection<Message>();

        public static MessageSource GetSource(Cloud cloud)
        {
            return Sources.ContainsKey(cloud.Id) ? Sources[cloud.Id] : Sources[cloud.Id] = new MessageSource();
        }

        public static MessageSource GetSource(string id)
        {
            return Sources.ContainsKey(id) ? Sources[id] : Sources[id] = new MessageSource();
        }

        public void AddMessages(Message message)
        {
            Messages.Add(message);
        }
    }
}
