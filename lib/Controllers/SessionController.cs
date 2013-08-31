using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Models.Client;
using CloudsdaleWin7.lib.Providers;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class SessionController : ISessionProvider, IMessageReceiver
    {
        private LastSession lastSession;
        private readonly List<Session> pastSessions = new List<Session>();

        public string LastSession
        {
            get { return lastSession.UserId; }
        }

        public Session CurrentSession { get; private set; }

        public IReadOnlyList<Session> PastSessions
        {
            get { return new ReadOnlyCollection<Session>(pastSessions); }
        }

        public void OnMessage(JObject message)
        {
            var user = message["data"].ToObject<Session>();
            user.CopyTo(CurrentSession);
        }
    }
}
