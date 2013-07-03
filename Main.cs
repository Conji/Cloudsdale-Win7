using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cloudsdale.actions;
using Cloudsdale.actions.MessageController;
using Cloudsdale.connection;

namespace Cloudsdale
{
    public partial class Main : Form
    {

        public bool SettingsVisible;
        public static Main Instance;
        public static JObject User;
        public static JToken CurrentCloud;
        public static Regex LinkRegex = new Regex(@"(?i)\b((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            LoginPanel.BringToFront();
            LoginPanel.Dock = DockStyle.Fill;
            if (LoginPanel.Visible == true)
            {
                this.MaximizeBox = false;
            }
            Subscriber.Visible = true;

        }
        //private void ViewTimer_Tick(object sender, EventArgs e)
        //{
        //    var SettingsNewSize = SettingsPanel.Height;
        //    if (SettingsNewSize >= 230)
        //    {
        //        SettingsPanel.Height = 230;
        //        ViewTimer.Stop();
        //    } else {
        //        SettingsNewSize += 1 * ((SettingsNewSize + 2) / 2);
        //        SettingsPanel.Height = SettingsNewSize;
        //    }
        //}

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Login.Enabled = false;
                Register.Enabled = false;
                Email.ReadOnly = true;
                Password.ReadOnly = true;
                await LoginRequest();
                this.AcceptButton = SendMessage;
                this.MaximizeBox = true;
                Connection.MessageReceived += o =>
                {
                    if (o["data"] == null) return;
                    var cloudId = ((string)o["channel"]).Split('/')[2];
                    LogMessage(o["data"], cloudId);
                    var source = MessageSource.GetSource(cloudId);
                    LoadMessageToSource(source, o["data"], cloudId);
                };
                await Connection.InitializeAsync();
                await PreloadMessages((JArray)Main.User["user"]["clouds"]);
                Login.Enabled = true;
                Register.Enabled = true;
                Email.ReadOnly = false;
                Password.ReadOnly = false;
                LoginPanel.Visible = false;
            }
            catch (CouldNotLoginException ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }

        }
        public async Task LoginRequest()
        {
            Main LoginInfo = new Main();
            var dataObject = new JObject();
            dataObject["email"] = Email.Text;
            dataObject["password"] = Password.Text;
            var data = Encoding.UTF8.GetBytes(dataObject.ToString(Formatting.None));

            var request = WebRequest.CreateHttp(Endpoints.Session);
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var requestStream = await request.GetRequestStreamAsync())
            {
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
                    }
                }
                throw new CouldNotLoginException(responseData);
            }

            var responseObject = JObject.Parse(responseData);
            Main.User = (JObject)responseObject["result"];
            await Connection.InitializeAsync();

        }
        private void LoadMessageToSource(MessageSource source, JToken message, string cloudId)
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
                    source.Messages.RemoveAt(source.Messages.Count - 1);
                    source.Messages.Add(lastMsg);
                }
                else
                {
                    message["content"] = message["content"]
                        .ToString().RegexReplace("^/me", (string)message["author"]["name"]);
                    source.AddMessage(message);
                }
            }

            if (Main.CurrentCloud != null && cloudId == (string)Main.CurrentCloud["id"])
            {
                MessageQueue.Queue.Enqueue(LinkRegex.Replace((string)message["content"], "")
                    //.Truncate(maxchars)
                    .Replace("\\n", " ")
                    .Replace("\\t", " ")
                    .Replace(":)", " smiley face ")
                    .Replace(":(", " sad face ")
                    .Replace(":D", " happy face ")
                    .Replace("<3", "♥")
                    .Replace("♥", " heart ")
                    .RegexReplace("^/me", (string)message["author"]["name"])
                    );
            }
        }
        private void LogMessage(JToken message, string cloudId)
        {
            var cloud = Main.User["user"]["clouds"].First(pcloud => (string)pcloud["id"] == cloudId);
            var log = new JObject();
            log["cloud"] = new JObject();
            log["cloud"]["name"] = cloud["name"];
            log["cloud"]["id"] = cloud["id"];
            log["user"] = new JObject();
            log["user"]["name"] = message["author"]["name"];
            log["user"]["id"] = message["author"]["id"];
            log["message"] = message["content"];
            if (message["drops"] != null && ((JArray)message["drops"]).Count > 0)
            {
                log["drops"] = message["drops"];
            }

            File.AppendAllText("./log.txt", log.ToString(Formatting.None, null) + ",\r\n");
        }
        private async Task PreloadMessages(ICollection<JToken> clouds)
        {
            foreach (var cloud in clouds)
            {
                var request = WebRequest.CreateHttp("http://www.cloudsdale.org/v1/clouds/" + cloud["id"] + "/chat/messages");
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
                            LoadMessageToSource(source, message, (string)cloud["id"]);
                        }
                        FayeConnector.Subscribe("/clouds/" + cloud["id"] + "/chat/messages");
                        CloudList.Items.Add(cloud["name"].ToString());
                    }
                }
            }
        }
        private void LaunchReg(object sender, EventArgs e)
        {
            Process.Start("https://www.cloudsdale.org/register");
        }
        private void CloudList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CloudList.SelectedItems.Count > 0)
            {
                MessageGroup.Text = CloudList.FocusedItem.Text;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessagePanel.Visible = false;
            MessageGroup.Text = "Welcome back!";
        }
    }
}

