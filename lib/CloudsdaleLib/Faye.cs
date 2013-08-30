using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocket4Net;

namespace CloudsdaleWin7.lib.CloudsdaleLib {
    static class FayeConnector {
        public static WebSocket Socket;
        private static string _clientId;
        public static event Action DoneConnecting;
        public static event Action LostConnection;

        public static string ClientId
        {
            get { return _clientId; }
        }

        public static async Task ConnectAsync() {
            var waiter = new ManualResetEvent(false);
            DoneConnecting += (new Action(() => waiter.Set()));
            Connect();
            await Task.Run(() => waiter.WaitOne());
        }

        public static void Connect() {
            if (Socket != null && Socket.State == WebSocketState.Open) Socket.Close();
            Socket = new WebSocket(Endpoints.PushAddress);
            Socket.Opened += OnOpen;
            Socket.MessageReceived += MessageReceived;
            Socket.Closed += (sender, args) => { if (LostConnection != null) LostConnection(); };
            Socket.Open();
        }

        static void OnOpen(object sender, EventArgs eventArgs) {
            var handshake = new JObject();
            handshake["channel"] = "/meta/handshake";
            handshake["version"] = "1.0";
            handshake["minimumVersion"] = "1.0beta";
            handshake["supportedConnectionTypes"] = new JArray { "websocket" };
            Socket.Send(handshake.ToString());
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
            _clientId = (string)handshake["clientId"];
            ConnectRequest();
            if (DoneConnecting != null) DoneConnecting();
        }

        public static void Publish(string channel, JToken data, JToken ext = null) {
            var message = new JObject();
            message["channel"] = channel;
            message["data"] = data;
            message["clientId"] = _clientId;
            if (ext != null) {
                message["ext"] = ext;
            } else {
                message["ext"] = new JObject();
            }
            message["ext"]["auth_token"] = MainWindow.User["user"]["auth_token"];
            Socket.Send(message.ToString());
        }

        public static void Subscribe(string channel) {
            var message = new JObject();
            message["channel"] = "/meta/subscribe";
            message["clientId"] = _clientId;
            message["subscription"] = channel;
            message["ext"] = new JObject();
            message["ext"]["auth_token"] = MainWindow.User["user"]["auth_token"];
            Socket.Send(message.ToString());
        }

        public static void Unsubscribe(string channel) {
            var message = new JObject();
            message["channel"] = "/meta/unsubscribe";
            message["clientId"] = _clientId;
            message["subscription"] = channel;
            Socket.Send(message.ToString());
        }

        static void ConnectRequest() {
            var connect = new JObject();
            connect["channel"] = "/meta/connect";
            connect["clientId"] = _clientId;
            connect["connectionType"] = "websocket";
            Socket.Send(connect.ToString());
        }
    }
    public interface IMessageReceiver
    {
        void OnMessage(JObject message);
    }
    public delegate void FayeCallback(MessageHandler handler, JObject response);
    public abstract class MessageHandler
    {
        private readonly JObject _extensionData = new JObject();
        private readonly DateTime _creationDate = new DateTime();

        public event FayeCallback Handshaked;
        public event FayeCallback Subscribed;
        public event FayeCallback Unsubscribed;
        public event FayeCallback Published;
        public event FayeCallback MessageReceived;
        public event Action Disconnected;

        public event EventHandler<JObject> HandshakedEvent;
        public event EventHandler<JObject> SubscribedEvent;
        public event EventHandler<JObject> UnsubscribedEvent;
        public event EventHandler<JObject> PublishedEvent;
        public event EventHandler<JObject> MessageReceivedEvent;

        public virtual JObject ExtensionData { get { return _extensionData; } }
        public abstract string ClientId { get; }
        public abstract bool IsConnecting { get; }
        public abstract bool IsConnected { get; }
        public IMessageReceiver PrimaryReciever { get; set; }
        public DateTime CreationDate { get { return _creationDate; } }

        internal Uri Address;

        public abstract bool IsSubscribed(string channel);

        public abstract void Connect();
        public abstract Task ConnectAsync();

        public abstract void Subscribe(string channel);
        public abstract void Unsubscribe(string channel);
        public abstract void Publish(string channel, JObject data);

        protected virtual void HandshakeCallback(JObject message) { }
        protected virtual void ConnectCallback(JObject message) { }
        protected virtual void SubscribeCallback(JObject message) { }
        protected virtual void UnsubscribeCallback(JObject message) { }
        protected virtual void PublishCallback(JObject message) { }

        protected void ProcessMessage(JToken message)
        {
            var channel = (string)message["channel"];
            var chansplit = channel.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (chansplit.Length < 2) return;

            if (chansplit[0] == "meta")
            {
                switch (chansplit[1])
                {
                    case "handshake":
                        HandshakeCallback((JObject)message);
                        break;
                    case "connect":
                        ConnectCallback((JObject)message);
                        break;
                    case "subscribe":
                        SubscribeCallback((JObject)message);
                        break;
                    case "unsubscribe":
                        UnsubscribeCallback((JObject)message);
                        break;
                }
                return;
            }

            if (message["successful"] != null)
            {
                PublishCallback((JObject)message);
            }

            if (PrimaryReciever != null)
            {
                PrimaryReciever.OnMessage((JObject)message);
            }
            OnReceive((JObject)message);
        }

        protected void OnHandshaked(JObject data)
        {
            var handler = Handshaked;
            if (handler != null) handler(this, data);
            var handlerE = HandshakedEvent;
            if (handlerE != null) handlerE(this, data);
        }
        protected void OnSubscribed(JObject data)
        {
            var handler = Subscribed;
            if (handler != null) handler(this, data);
            var handlerE = SubscribedEvent;
            if (handlerE != null) handlerE(this, data);
        }
        protected void OnUnsubscribed(JObject data)
        {
            var handler = Unsubscribed;
            if (handler != null) handler(this, data);
            var handlerE = UnsubscribedEvent;
            if (handlerE != null) handlerE(this, data);
        }
        protected void OnPublished(JObject data)
        {
            var handler = Published;
            if (handler != null) handler(this, data);
            var handlerE = PublishedEvent;
            if (handlerE != null) handlerE(this, data);
        }
        protected void OnReceive(JObject data)
        {
            var handler = MessageReceived;
            if (handler != null) handler(this, data);
            var handlerE = MessageReceivedEvent;
            if (handlerE != null) handlerE(this, data);
        }
        protected void OnDisconnect()
        {
            var handler = Disconnected;
            if (handler != null) handler();
        }
    }
}
