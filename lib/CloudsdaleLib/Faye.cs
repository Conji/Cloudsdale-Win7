using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace CloudsdaleWin7.lib.CloudsdaleLib {
    
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
    public static class Faye
    {
        public static MessageHandler CreateClient(Uri address)
        {
            var handler = new WebsocketHandler { Address = address };
            return handler;
        }
    }
    internal class WebsocketHandler : MessageHandler
    {
        private string _clientId;
        private HandlerState _state;

        #region Implementation
        public override string ClientId
        {
            get { return _clientId; }
        }

        public override bool IsConnecting
        {
            get { return _state.connecting && !_state.connected && !_state.closed; }
        }

        public override bool IsConnected
        {
            get { return _state.connecting && _state.connected && !_state.closed; }
        }

        public override bool IsSubscribed(string channel)
        {
            return _state.subbedChannels.ToList().Any(chan => MatchChannel(chan, channel));
        }

        public override async Task ConnectAsync()
        {
            var handshakeWaiter = new EventWaiter();
            Handshaked += handshakeWaiter.Callback;

            await Connect(Address);
            await handshakeWaiter.Wait();

            Handshaked -= handshakeWaiter.Callback;
        }
        public override async void Connect()
        {
            await Connect(Address);
        }
        private async Task Connect(Uri address)
        {
            InitState();

            
            _state.connecting = true;
            _state.socket.BeginConnect(address.OriginalString, 80, null, _state.connecting);
            _state.connected = true;

            var handshake = new JObject();
            handshake["channel"] = "/meta/handshake";
            handshake["version"] = "1.0";
            handshake["minimumVersion"] = "1.0";
            handshake["supportedConnectionTypes"] = JArray.FromObject(new[] { "websocket" });

            await SendNoId(handshake);
        }

        public override async void Subscribe(string channel)
        {
            var request = new JObject();
            request["channel"] = "/meta/subscribe";
            request["subscription"] = channel;

            await Send(request);
        }

        public override async void Unsubscribe(string channel)
        {
            var request = new JObject();
            request["channel"] = "/meta/unsubscribe";
            request["subscription"] = channel;

            await Send(request);
        }

        public override async void Publish(string channel, JObject data)
        {
            var request = new JObject();
            request["channel"] = channel;
            request["data"] = data;

            await Send(request);
        }
        #endregion

        #region Internals
        public async Task Send(JObject data)
        {
            data["clientId"] = ClientId;
            await SendNoId(data);
        }

        public async Task SendNoId(JObject data)
        {
            if (ExtensionData != null)
            {
                data["ext"] = ExtensionData;
            }

            using (var writer = new StreamWriter(new NetworkStream(_state.socket)))
            {
                await writer.WriteAsync(data.ToString());
                await writer.FlushAsync();
                writer.Dispose();
            }
        }

        public void InitState()
        {
            _state = new HandlerState
            {
                socket = new Socket(SocketType.Stream, ProtocolType.IP),
                subbedChannels = new List<string>()
            };
        }

        public bool MatchChannel(string pattern, string channel)
        {
            var psplit = pattern.Split('/');
            var csplit = channel.Split('/');

            if (csplit.Length < psplit.Length) return false;

            for (var i = 0; i < psplit.Length; ++i)
            {
                if (psplit[i] == "**") return true;
                if (psplit[i] != "*" && psplit[i] != csplit[i]) return false;
            }

            return csplit.Length == psplit.Length;
        }

        public async void ConnectRequest()
        {
            var request = new JObject();
            request["channel"] = "/meta/connect";
            request["connectionType"] = "websocket";

            await Send(request);
        }
        #endregion

        #region Callbacks
        protected override void HandshakeCallback(JObject message)
        {
            if (!(bool)message["successful"])
            {
                _state.socket.Dispose();
                _state.closed = true;
                OnDisconnect();
            }

            _clientId = (string)message["clientId"];
            OnHandshaked(message);

            ConnectRequest();
        }

        protected override void ConnectCallback(JObject message)
        {
            ConnectRequest();
        }

        protected override void PublishCallback(JObject message)
        {
            OnPublished(message);
        }

        protected override void SubscribeCallback(JObject message)
        {
            if ((bool)message["successful"])
            {
                _state.subbedChannels.Add((string)message["subscription"]);
            }
            OnSubscribed(message);
        }

        protected override void UnsubscribeCallback(JObject message)
        {
            _state.subbedChannels.Remove((string)message["subscription"]);
            OnUnsubscribed(message);
        }
        #endregion

        internal struct HandlerState
        {
            public Socket socket;
            public bool connecting;
            public bool connected;
            public bool closed;

            public List<string> subbedChannels;
        }
        internal class EventWaiter
        {
            private readonly ManualResetEvent waiter = new ManualResetEvent(false);

            public async Task Wait()
            {
                await Task.Run(() => waiter.WaitOne());
            }

            public void Callback(MessageHandler handler, JObject response)
            {
                waiter.Set();
            }
        }
    }
}
