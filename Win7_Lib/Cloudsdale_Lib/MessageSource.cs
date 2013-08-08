using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7.Cloudsdale_Lib {
    public class MessageSource {
        private static readonly Dictionary<string, MessageSource> Sources = new Dictionary<string, MessageSource>(); 
        public readonly ObservableCollection<JToken> Messages = new ObservableCollection<JToken>();

        public static MessageSource GetSource(JToken cloud) {
            if (Sources.ContainsKey((string)cloud["id"])) {
                return Sources[(string) cloud["id"]];
            }
            return Sources[(string) cloud["id"]] = new MessageSource();
        }
        public static MessageSource GetSource(string cloudId) {
            if (Sources.ContainsKey(cloudId)) {
                return Sources[cloudId];
            }
            return Sources[cloudId] = new MessageSource();
        }

        public void AddMessage(JToken message) {
            if (MainWindow.Instance.Dispatcher.CheckAccess()) {
                Messages.Add(message);
            } else {
                MainWindow.Instance.Dispatcher.BeginInvoke(new Action(() => Messages.Add(message)));
            }
        }
    }
}
