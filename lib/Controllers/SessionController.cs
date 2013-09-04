using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;
using CloudsdaleWin7.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class SessionController : ISessionProvider, IMessageReceiver
    {
        private readonly List<Session> _pastSessions = new List<Session>();

        public Session CurrentSession { get; set; }

        public IReadOnlyList<Session> PastSessions
        {
            get { return new ReadOnlyCollection<Session>(_pastSessions); }
        }

        public void OnMessage(JObject message)
        {
            var user = message["data"].ToObject<Session>();
            user.CopyTo(CurrentSession);
        }
        public async Task Login(string email, string password)
        {

            var requestModel = @"{""email"":""[:email]"", ""password"":""[:password]""".Replace("[:email]", email).Replace("[:password]", password);
            var request = new HttpClient().AcceptsJson();
            var result = request.PostAsync(Endpoints.Session, new JsonContent(requestModel));
            var resultString = await result.Result.Content.ReadAsStringAsync();
            var response = await JsonConvert.DeserializeObjectAsync<WebResponse<SessionWrapper>>(resultString);
            CurrentSession = response.Result.User;
        }

        public async Task Logout()
        {
            MainWindow.Instance.MainFrame.Navigate(new Login());
            CurrentSession = null;
        }

        public class SessionWrapper
        {
            public string ClientID { get; set; }
            public Session User { get; set; }
        }
    }
}
