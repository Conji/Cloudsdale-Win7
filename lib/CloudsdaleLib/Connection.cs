using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib {
    public static class Connection {
        public static event Action<JObject> MessageReceived;

        public static void Initialize() {
            Faye.LostConnection += Faye.Connect;
            Faye.DoneConnecting += delegate {
                foreach (var cloud in MainWindow.User["user"]["clouds"]) {
                    Faye.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
                }
            };
            Faye.Connect();
        }

        public static async Task InitializeAsync() {
            Faye.LostConnection += Faye.Connect;
            await Faye.ConnectAsync();
        }

        internal static void OnMessageReceived(JObject obj) {
            if (MessageReceived != null) MessageReceived(obj);
        }
    }
}
