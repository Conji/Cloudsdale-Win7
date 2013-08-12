using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.ErrorConsole;
using CloudsdaleWin7.lib.ErrorConsole.CConsole;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public static Login Instance;
        public static bool LoggingOut = false;

        public Login()
        {
            Instance = this;
            InitializeComponent();
            EmailBox.Text = UserSettings.Default.PreviousEmail;
            PasswordBox.Password = UserSettings.Default.PreviousPassword;
            autoSession.IsChecked = UserSettings.Default.AutoLogin;
            if ((EmailBox.Text + PasswordBox.Password) == "")
            {
                EmailBox.Foreground = new SolidColorBrush(Colors.DarkGray);
                PasswordBox.Foreground = new SolidColorBrush(Colors.DarkGray);
                EmailBox.Text = "email";
                PasswordBox.Password = "password";
            }else
            {
                EmailBox.Foreground = new SolidColorBrush(Colors.Black);
                PasswordBox.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (LoggingOut == false)
            {
                if (autoSession.IsChecked == true)
                {
                    LoginClick(LoginButton, null);
                }
            }
        }

        public static void Logout()
        {
            LoggingOut = true;
            Instance.EmailBox.Text = UserSettings.Default.PreviousEmail;
            Instance.PasswordBox.Password = UserSettings.Default.PreviousPassword;
            Instance.autoSession.IsChecked = false;
        }

        private static readonly Regex LinkRegex = new Regex(@"(?i)\b((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            MainLayout.Visibility = Visibility.Collapsed;
            LoggingInUI.Visibility = Visibility.Visible;
            LoginButton.IsEnabled = false;
            LogginContents.Visibility = Visibility.Collapsed;

            try {
                await EmailLogin();
                LoginButton.IsDefault = false;
                UserSettings.Default.PreviousEmail = EmailBox.Text;
                UserSettings.Default.PreviousPassword = PasswordBox.Password;
                UserSettings.Default.AutoLogin = autoSession.IsChecked.Value;
                UserSettings.Default.Save();
                
            } catch (Exception ex) {
                LoginButton.IsEnabled = true;
                MainLayout.Visibility = Visibility.Visible;
                LoggingInUI.Visibility = Visibility.Collapsed;
                LogginContents.Visibility = Visibility.Visible;
                MessageBox.Show(ex.ToString());
                return;
            }

            Connection.MessageReceived += o => {
                if (o["data"] == null) return;
                var cloudId = ((string)o["channel"]).Split('/')[2];
                var source = MessageSource.GetSource(cloudId);
                LoadMessageToSource(source, o["data"], cloudId);
            };
            await Connection.InitializeAsync();
            try
            {
                await PreloadMessages((JArray)MainWindow.User["user"]["clouds"]);
            }catch(Exception ex)
            {
                WriteError.Write(ex.Message);
            }
            

            MainWindow.Instance.CloudList.ItemsSource = MainWindow.User["user"]["clouds"];
            MainWindow.Instance.Frame.Navigated += NavToHomeCallback;
            MainWindow.Instance.Frame.Navigate(new Home());
        }

        private void LoadMessageToSource(MessageSource source, JToken message, string cloudId) {
            message["orgcontent"] = message["content"] =
                message["content"].ToString().UnescapeLiteral().RegexReplace(@"[ ]+", " ");
            lock (source) {
                JToken lastMsg;
                if (source.Messages.Any()
                    && (string)(lastMsg = source.Messages.Last())["author"]["id"] == (string)message["author"]["id"]
                    && !lastMsg["orgcontent"].ToString().StartsWith("/me")
                    && !message["content"].ToString().StartsWith("/me")) {
                    lastMsg["content"] += "\n" + message["content"];
                    lastMsg["drops"] = new JArray(lastMsg["drops"].Concat(message["drops"]));
                    Dispatcher.Invoke(() => {
                        source.Messages.RemoveAt(source.Messages.Count - 1);
                        source.Messages.Add(lastMsg);
                    });
                } else {
                    message["content"] = message["content"]
                        .ToString().RegexReplace("^/me", (string)message["author"]["name"]);
                    source.AddMessage(message);
                }
            }
        }

        private static void NavToHomeCallback(object o, EventArgs e) {
            MainWindow.Instance.Frame.Navigated -= NavToHomeCallback;
            MainWindow.Instance.Frame.RemoveBackEntry();
        }

        private void SetStatus(string text) {
            Dispatcher.Invoke(() => LoadStatus.Text = text);
        }
        private void SetProgress(double progress) {
            Dispatcher.Invoke(() => LoadProgress.Value = progress);
        }

        private async Task EmailLogin() {
            SetStatus("Logging in...");

            var dataObject = new JObject();
            dataObject["email"] = EmailBox.Text;
            dataObject["password"] = PasswordBox.Password;
            var data = Encoding.UTF8.GetBytes(dataObject.ToString(Formatting.None));

            var request = WebRequest.CreateHttp(Endpoints.Session);
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var requestStream = await request.GetRequestStreamAsync()) {
                await requestStream.WriteAsync(data, 0, data.Length);
                await requestStream.FlushAsync();
                requestStream.Close();
            }

            string responseData;
            try
            {
                using (var response = await request.GetResponseAsync())
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null) return;
                    using (var responseStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        responseData = await responseStreamReader.ReadToEndAsync();
                    }
                }
            }
            catch (WebException ex)
            {
                using (var response = ex.Response)
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null) return;
                    using (var responseStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        responseData = responseStreamReader.ReadToEnd();
                        var console = new ErrorConsole();
                        console.Show();
                        WriteError.Write(responseData);
                    }
                }
            }
            var responseObject = JObject.Parse(responseData);
            MainWindow.User = (JObject)responseObject["result"];
        }

        private async Task PreloadMessages(ICollection<JToken> clouds) {
            SetStatus("Loading cloud messages...");
            double i = 0;
            var cloudCount = clouds.Count;
            foreach (var cloud in clouds) {
                SetProgress(100 * ++i / cloudCount);
                SetStatus("Loading cloud messages for " + cloud["name"] + "...");
                var request = WebRequest.CreateHttp(Endpoints.CloudMessages.Replace("[:id]", (string)cloud["id"]));
                request.Accept = "application/json";
                using (var response = await request.GetResponseAsync())
                using (var responseStream = response.GetResponseStream()) {
                    if (responseStream == null) continue;
                    using (var responseReader = new StreamReader(responseStream)) {
                        var responseData = JObject.Parse(await responseReader.ReadToEndAsync());
                        var source = MessageSource.GetSource(cloud);
                        foreach (var message in responseData["result"]) {
                            LoadMessageToSource(source, message, (string)cloud["id"]);
                        }
                        FayeConnector.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
                    }
                }
            }
        }

        private void ColorChange(object sender, TextChangedEventArgs e)
        {
            if (EmailBox.Text == "email")
            {
                EmailBox.Foreground = new SolidColorBrush(Colors.DarkGray);
                PasswordBox.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
            else
            {
                EmailBox.Foreground = new SolidColorBrush(Colors.Black);
                PasswordBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "email")
            {
                EmailBox.Text = "";
                PasswordBox.Password = "";
            }
        }

        private void AutoFill(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "")
            {
                EmailBox.Text = "email";
                PasswordBox.Password = "";
            }
        }
    }
}
