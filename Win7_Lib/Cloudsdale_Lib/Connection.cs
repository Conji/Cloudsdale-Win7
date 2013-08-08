using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7.Win7_Lib {
    public static class Connection {
        public static event Action<JObject> MessageReceived;

        public static void Initialize() {
            FayeConnector.LostConnection += FayeConnector.Connect;
            FayeConnector.DoneConnecting += delegate {
                foreach (var cloud in MainWindow.User["user"]["clouds"]) {
                    FayeConnector.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
                }
            };
            FayeConnector.Connect();
        }

        public static async Task InitializeAsync() {
            FayeConnector.LostConnection += FayeConnector.Connect;
            await FayeConnector.ConnectAsync();
        }

        internal static void OnMessageReceived(JObject obj) {
            if (MessageReceived != null) MessageReceived(obj);
        }
    }
}
