using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.ErrorConsole.CConsole;
using CloudsdaleWin7.lib.Faye;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.Views.LoadingViews
{
    /// <summary>
    /// Interaction logic for LoadLogin.xaml
    /// </summary>
    public partial class LoadLogin : Page
    {
        public LoadLogin()
        {
            InitializeComponent();
            LoadingMovie.Play();
            Connection.MessageReceived += o =>
            {
                if (o["data"] == null) return;
                var cloudId = ((string)o["channel"]).Split('/')[2];
                var source = MessageSource.GetSource(cloudId);
                LoadMessageToSource(source, o["data"]);
            };
            Connection.Initialize();
            try
            {
                PreloadMessages((JArray)MainWindow.User["user"]["clouds"]);
            }
            catch (Exception ex)
            {
                WriteError.ShowError(ex.Message);
            }
            //add cloudlistSource
        }
        private void LoadMessageToSource(MessageSource source, JToken message)
        {
            message["orgcontent"] = message["content"] =
                message["content"].ToString().UnescapeLiteral().RegexReplace(@"[ ]+", " ");
            lock (source)
            {
                JToken lastMsg;
                if (source.Messages.Any()
                    && (string)(lastMsg = source.Messages.Last())["author"]["id"] == (string)message["author"]["id"]
                    && !lastMsg["orgcontent"].ToString().StartsWith("/me")
                    && !message["content"].ToString().StartsWith("/me"))
                {
                    lastMsg["content"] += "\n" + message["content"];
                    lastMsg["drops"] = new JArray(lastMsg["drops"].Concat(message["drops"]));
                    Dispatcher.Invoke(() =>
                    {
                        source.Messages.RemoveAt(source.Messages.Count - 1);
                        source.Messages.Add(lastMsg);
                    });
                }
                else
                {
                    message["content"] = message["content"]
                        .ToString().RegexReplace("^/me", (string)message["author"]["name"]);
                    source.AddMessage(message);
                }
            }
        }

        private async Task PreloadMessages(ICollection<JToken> clouds)
        {

            var cloudCount = clouds.Count;
            foreach (var cloud in clouds)
            {
                LoadingMessage.Text = "Loading messages for " + cloud["name"] + "...";
                var request = WebRequest.CreateHttp(Endpoints.CloudMessages.Replace("[:id]", (string)cloud["id"]));
                request.Accept = "application/json";
                using (var response = await request.GetResponseAsync())
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null) continue;
                    using (var responseReader = new StreamReader(responseStream))
                    {
                        var responseData = JObject.Parse(await responseReader.ReadToEndAsync());
                        var source = MessageSource.GetSource(cloud);
                        foreach (var message in responseData["result"])
                        {
                            LoadMessageToSource(source, message);
                        }
                        App.Connection.Faye.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
                    }
                }
            }
            LoadingMovie.Stop();
        }

        private void Loop(object sender, RoutedEventArgs e)
        {
            LoadingMovie.Position = TimeSpan.Zero;
            LoadingMovie.Play();
        }
    }
}
