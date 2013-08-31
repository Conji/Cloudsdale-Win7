using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib
{
    public static class Connection
    {
        public static event Action<JObject> MessageReceived;
        
        private static MessageHandler FayeClient = new WebsocketHandler();
        public static void Initialize()
        {
            
            foreach (var cloud in MainWindow.User["user"]["clouds"])
            {
                
                FayeClient.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
            }
            FayeClient.Connect();
        }

        internal static void OnMessageReceived(JObject obj)
        {
            if (MessageReceived != null) MessageReceived(obj);
        }
    }
}
