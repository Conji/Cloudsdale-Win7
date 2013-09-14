using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CloudsdaleWin7.Views.Initial;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;
using CloudsdaleWin7.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Controllers
{
    public class SessionController : ISessionProvider
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
            try
            {
                CurrentSession = response.Result.User;
                RegistrationCheck();
            }
            catch
            {
                CloudsdaleWin7.Login.Instance.LoggingInUi.Visibility = Visibility.Hidden;
                CloudsdaleWin7.Login.Instance.LoginUi.Visibility = Visibility.Visible;
                CloudsdaleWin7.Login.Instance.ShowMessage(response.Flash.Message);
            }
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
        private void InitializeClouds()
        {
            foreach (var cloud in CurrentSession.Clouds)
            {

                App.Connection.MessageController.CloudControllers.Add(cloud.Id, new CloudController(cloud));
                
            }
        }
        private void RegistrationCheck()
        {
            if (CurrentSession.NeedsToConfirmRegistration == true)
            {
                MainWindow.Instance.MainFrame.Navigate(new Confirm());
            }
            else
            {
                MainWindow.Instance.MainFrame.Navigate(new Main());
                InitializeClouds();
            }

        }
    }
}
