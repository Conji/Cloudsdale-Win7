using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using CloudsdaleWin7.Properties;
using CloudsdaleWin7.Views.Initial;
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

        public Session CurrentSession { get; set; }

        public void OnMessage(JObject message)
        {
            var user = message["data"].ToObject<Session>();
            user.CopyTo(CurrentSession);
        }

        public async Task Login(string semail, string spassword)
        {
            var requestModel = new
                               {
                                   email = semail,
                                   password = spassword
                               };
            var request = new HttpClient().AcceptsJson();
            var result = await request.PostAsync(Endpoints.Session, new JsonContent(requestModel));
            var response = await JsonConvert.DeserializeObjectAsync<WebResponse<SessionWrapper>>(await result.Content.ReadAsStringAsync());
            try
            {
                if (response.Flash != null)
                {
                    CloudsdaleWin7.Login.Instance.LoggingInUi.Visibility = Visibility.Hidden;
                    CloudsdaleWin7.Login.Instance.LoginUi.Visibility = Visibility.Visible;
                    CloudsdaleWin7.Login.Instance.ShowMessage(response.Flash.Message);
                    return;
                }
                CurrentSession = response.Result.User;
                App.Settings["token"] = CurrentSession.AuthToken;
                App.Settings["id"] = CurrentSession.Id;
                InitializeClouds();
                RegistrationCheck();
            }
            catch
            {
                CloudsdaleWin7.Login.Instance.LoggingInUi.Visibility = Visibility.Hidden;
                CloudsdaleWin7.Login.Instance.LoginUi.Visibility = Visibility.Visible;
                CloudsdaleWin7.Login.Instance.ShowMessage(response.Flash != null ? response.Flash.Message : "Oops! An error occured that we couldn't seem to detect!");
            }
        }

        public void Logout()
        {
            FayeConnector.Socket.Close();
            FayeConnector.Socket.Closed += (sender, args) =>
            {
                App.Connection.MessageController.CloudControllers = new Dictionary<string, CloudController>();
                CurrentSession = null;
                App.Settings.Clear();
            };
            MainWindow.Instance.MainFrame.Navigate(new Login());
        }

        public void RefreshClouds()
        {
            Main.Instance.Clouds.ItemsSource = null;
            Main.Instance.Clouds.ItemsSource = CurrentSession.Clouds;
        }

        public class SessionWrapper
        {
            public string ClientId { get; set; }
            public Session User { get; set; }
        }

        private async void InitializeClouds()
        {
            foreach (var cloud in CurrentSession.Clouds.Where(c => !App.Connection.MessageController.CloudControllers.Keys.Contains(c.Id)))
            {
                App.Connection.MessageController.CloudControllers.Add(cloud.Id, new CloudController(cloud));
                await App.Connection.MessageController[cloud].LoadMessages(false);
                //await App.Connection.MessageController[cloud].LoadUsers();
            }
        }

        private void RegistrationCheck()
        {
            if (CurrentSession.NeedsToConfirmRegistration == true) MainWindow.Instance.MainFrame.Navigate(new Confirm());
            else
            {
                MainWindow.Instance.MainFrame.Navigate(new Main());
                InitializeClouds();
            }

        }
    }
}
