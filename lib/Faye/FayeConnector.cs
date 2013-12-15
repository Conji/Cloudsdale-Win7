using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocket4Net;

namespace CloudsdaleWin7.lib.Faye
{
    static class FayeConnector
    {
        public static WebSocket Socket;
        public static event Action DoneConnecting;
        public static event Action LostConnection;
        public static bool Connected = false;

        public static string ClientId { get; private set; }

        public static async Task ConnectAsync()
        {
            var waiter = new ManualResetEvent(false);
            DoneConnecting += () => waiter.Set();
            Connect();
            await Task.Run(() => waiter.WaitOne());
        }

        public static void ForceClose()
        {
            Socket.Close();
            Socket.MessageReceived += null;
            Socket = null;
            Connected = false;
        }

        public static void Connect()
        {
            if (Socket != null && Socket.State == WebSocketState.Open) Socket.Close();
            Connected = false;
            Socket = new WebSocket(Endpoints.PushAddress);
            Socket.Opened += OnOpen;
            Socket.MessageReceived += MessageReceived;
            Socket.Closed += (sender, args) => { if (LostConnection != null) LostConnection(); };
            Socket.Open();
            Connected = true;
        }

        public static void Disconnect()
        {
            if (Socket != null && Socket.State == WebSocketState.Open)
            {
                Socket.Close();
            }
            Connected = false;
        }

        static void OnOpen(object sender, EventArgs eventArgs)
        {
            var handshake = new JObject();
            handshake["channel"] = "/meta/handshake";
            handshake["version"] = "1.0";
            handshake["minimumVersion"] = "1.0beta";
            handshake["supportedConnectionTypes"] = new JArray { "websocket" };
            Socket.Send(handshake.ToString());
        }

        static void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var packet = JArray.Parse(e.Message);
            foreach (JObject message in packet)
            {
                switch ((string)message["channel"])
                {
                    case "/meta/handshake":
                        HandshakeResponse(message);
                        break;
                    case "/meta/connect":
                        ConnectRequest();
                        break;
                    default:
                        if (((string)message["channel"]).StartsWith("/cloud") || ((string)message["channel"]).StartsWith("/cloud"))
                        {
                            Connection.OnMessageReceived(message);
                        }
                        
                        break;
                }
            }
        }

        static void HandshakeResponse(JToken handshake)
        {
            ClientId = (string)handshake["clientId"];
            ConnectRequest();
            if (DoneConnecting != null) DoneConnecting();
        }

        public static void Publish(string channel, JToken data, JToken ext = null)
        {
            var message = new JObject();
            message["channel"] = channel;
            message["data"] = data;
            message["clientId"] = ClientId;
            if (ext != null)
            {
                message["ext"] = ext;
            }
            else
            {
                message["ext"] = new JObject();
            }
            message["ext"]["auth_token"] = App.Connection.SessionController.CurrentSession.AuthToken;
            Socket.Send(message.ToString());
        }

        public static void Subscribe(string channel)
        {
            var message = new JObject();
            message["channel"] = "/meta/subscribe";
            message["clientId"] = ClientId;
            message["subscription"] = channel;
            message["ext"] = new JObject();
            message["ext"]["auth_token"] = App.Connection.SessionController.CurrentSession.AuthToken;
            Socket.Send(message.ToString());
        }

        public static void Unsubscribe(string channel)
        {
            var message = new JObject();
            message["channel"] = "/meta/unsubscribe";
            message["clientId"] = ClientId;
            message["subscription"] = channel;
            Socket.Send(message.ToString());
        }

        static void ConnectRequest()
        {
            var connect = new JObject();
            connect["channel"] = "/meta/connect";
            connect["clientId"] = ClientId;
            connect["connectionType"] = "websocket";
            Socket.Send(connect.ToString());
        }
    }
}
