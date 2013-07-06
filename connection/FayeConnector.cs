using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocket4Net;

namespace Cloudsdale.connection
{
    static class FayeConnector
    {
        public static WebSocket socket;
        private static string clientID;
        public static event Action DoneConnecting;
        public static event Action LostConnection;

        public static string ClientID {
            get { return clientID; }
        }

        public static async Task ConnectAsync() {
            var waiter = new ManualResetEvent(false);
            DoneConnecting += () => {
                waiter.Set();
            };
            Connect();
            await Task.Run(() => waiter.WaitOne());
        }

        public static void Connect() {
            if (socket != null && socket.State == WebSocketState.Open) socket.Close();
            socket = new WebSocket("ws://push01.cloudsdale.org/push");
            socket.Opened += OnOpen;
            socket.MessageReceived += MessageReceived;
            socket.Closed += (sender, args) => { if (LostConnection != null) LostConnection(); };
            socket.Open();
        }

        static void OnOpen(object sender, EventArgs eventArgs) {
            var handshake = new JObject();
            handshake["channel"] = "/meta/handshake";
            handshake["version"] = "1.0";
            handshake["minimumVersion"] = "1.0beta";
            handshake["supportedConnectionTypes"] = new JArray { "websocket" };
            socket.Send(handshake.ToString());
        }

        static void MessageReceived(object sender, MessageReceivedEventArgs e) {
            var packet = JArray.Parse(e.Message);
            foreach (JObject message in packet) {
                switch ((string)message["channel"]) {
                    case "/meta/handshake":
                        HandshakeResponse(message);
                        break;
                    case "/meta/connect":
                        ConnectRequest();
                        break;
                    default:
                        if (((string)message["channel"]).StartsWith("/cloud")) {
                            Connection.OnMessageReceived(message);
                        }
                        break;
                }
            }
        }

        static void HandshakeResponse(JToken handshake) {
            clientID = (string)handshake["clientId"];
            ConnectRequest();
            if (DoneConnecting != null) DoneConnecting();
        }

        public static void Publish(string channel, JToken data, JToken ext = null) {
            var message = new JObject();
            message["channel"] = channel;
            message["data"] = data;
            message["clientId"] = clientID;
            if (ext != null) {
                message["ext"] = ext;
            } else {
                message["ext"] = new JObject();
            }
            message["ext"]["auth_token"] = Main.User["user"]["auth_token"];
            socket.Send(message.ToString());
        }

        public static void Subscribe(string channel) {
            var message = new JObject();
            message["channel"] = "/meta/subscribe";
            message["clientId"] = clientID;
            message["subscription"] = channel;
            message["ext"] = new JObject();
            message["ext"]["auth_token"] = Main.User["user"]["auth_token"];
            socket.Send(message.ToString());
        }

        public static void Unsubscribe(string channel) {
            var message = new JObject();
            message["channel"] = "/meta/unsubscribe";
            message["clientId"] = clientID;
            message["subscription"] = channel;
            socket.Send(message.ToString());
        }

        static void ConnectRequest() {
            var connect = new JObject();
            connect["channel"] = "/meta/connect";
            connect["clientId"] = clientID;
            connect["connectionType"] = "websocket";
            socket.Send(connect.ToString());
        }
    }
    public static class Connection
    {
        public static event Action<JObject> MessageReceived;

        public static void Initialize()
        {
            FayeConnector.LostConnection += FayeConnector.Connect;
            FayeConnector.DoneConnecting += delegate
            {
                foreach (var cloud in Main.User["user"]["clouds"])
                {
                    FayeConnector.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
                }
            };
            FayeConnector.Connect();
        }

        public static async Task InitializeAsync()
        {
            FayeConnector.LostConnection += FayeConnector.Connect;
            await FayeConnector.ConnectAsync();
        }

        internal static void OnMessageReceived(JObject obj)
        {
            if (MessageReceived != null) MessageReceived(obj);
        }
    }
}