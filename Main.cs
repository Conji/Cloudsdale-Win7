using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using Cloudsdale.connection;
using Cloudsdale.connection.MessageController;
using Cloudsdale.connection;
using Cloudsdale.lib;
using Cloudsdale.lib.MessageUI;

namespace Cloudsdale
{
    public partial class Main : Form
    {

        public bool SettingsVisible;
        public static Main Instance = new Main();
        public static JObject User;
        public static JToken CurrentCloud;
        public static Regex LinkRegex = new Regex(@"(?i)\b((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public bool DirectedToHome;
        public bool LoggedIn;
        public CloudMessage NewMessage = new CloudMessage();
        public MessageList CloudMessages = new MessageList();
        

        public Main()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Email.Text = CloudsdaleSettings.Default.PreviousEmail;
            Password.Text = CloudsdaleSettings.Default.PreviousPassword;
            LoadAnimation.Visible = false;
            ImageAnimator.Animate(CloudAnimation.Image, null);
            LoggedIn = false;
            this.Width = 695;
            LoginPanel.BringToFront();
            LoginPanel.Dock = DockStyle.Fill;
            if (LoginPanel.Visible == true) { this.MaximizeBox = false; }
            Subscriber.Visible = true;
            SettingsPanel.Height = 0;

            CloudMessages.Width = 500;
            CloudMessages.Height = 300;
            CloudMessages.Visible = false;

            CloudMessages.BringToFront();
            MessageGroup.Controls.Add(CloudMessages);
            if (autologin.Checked)
            {
                if (Email.Text != null)
                {
                    button1_Click(Login, EventArgs.Empty);
                }
            }
        }
 
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CloudsdaleSettings.Default.PreviousEmail = Email.Text;
                CloudsdaleSettings.Default.PreviousPassword = Password.Text;
                CloudsdaleSettings.Default.Save();
                LoadAnimation.Visible = true;
                ImageAnimator.Animate(LoadAnimation.Image, null);
                this.Text = "Logging in...";
                Login.Enabled = false;
                Register.Enabled = false;
                Email.ReadOnly = true;
                Password.ReadOnly = true;
                await LoginRequest();
                this.Text = "Login succesful.";
                this.AcceptButton = m_SendMessage;
                this.MaximizeBox = true;
                Connection.MessageReceived += o =>
                {
                    if (o["data"] == null) return;
                    var cloudId = ((string)o["channel"]).Split('/')[2];
                    var source = MessageSource.GetSource(cloudId);
                    LoadMessageToSource(source, o["data"], cloudId);
                };
                await Connection.InitializeAsync();
                this.Text = "Loading clouds...";
                Main MainWindow = new Main();
                await PreloadMessages((JArray)User["user"]["clouds"]);
                h_avatar.ImageLocation = User["user"]["avatar"]["normal"].ToString();
                h_avatar.SizeMode = PictureBoxSizeMode.Zoom;
                Login.Enabled = true;
                Register.Enabled = true;
                Email.ReadOnly = false;
                Password.ReadOnly = false;
                LoginPanel.Visible = false;
                //UserCheck.Authorize(User["user"]["name"].ToString());
                //Discord's fault.
                MessageGroup.Text = "Welcome back, " + User["user"]["name"] + "!";
                var calDate = User["user"]["member_since"].ToString();
                var newDate = calDate.Replace("+0000", "");
                h_memberSince.Text = "Member since " + newDate;
     

                RedirectHome(true);
                LoggedIn = true;
            }
            catch (CouldNotLoginException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Login.Enabled = true;
                Register.Enabled = true;
                Email.ReadOnly = false;
                Password.ReadOnly = false;
                this.Text = "Cloudsdale";
            }
            catch (WebException webex)
            {
                System.Windows.MessageBox.Show(webex.Message);
                Login.Enabled = true;
                Register.Enabled = true;
                Email.ReadOnly = false;
                Password.ReadOnly = false;
                this.Text = "Cloudsdale";
            }
            LoadAnimation.Visible = false;
        }
        public async Task LoginRequest()
        {
            var dataObject = new JObject();
            dataObject["email"] = Email.Text;
            dataObject["password"] = Password.Text;
            var data = Encoding.UTF8.GetBytes(dataObject.ToString(Formatting.None));

            var request = WebRequest.CreateHttp(Endpoints.Session);
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 1500;
            
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
                
                if (LoggedIn)
                {

                    Subscriber.BalloonTipText = (string) message["author"]["name"] + " has posted in " +
                                                Endpoints.Cloud.Replace("[:id]", cloudId);
                    Subscriber.ShowBalloonTip(5);
                }
                //NewMessage.AddMessage(CloudMessages, (string)message["author"]["name"], (string)message["author"]["username"], (string)message["author"]["role"], (string)message["timestamp"], "online", "mobile", (string)message["content"], LoadImage((string)message["author"]["avatar"]["normal"]));
                if (CloudMessages.InvokeRequired)
                {
                    CloudMessages.Invoke(new Action(() =>
                    {
                        NewMessage.Width = CloudMessages.Width - 20;
                        NewMessage._name = (string)message["author"]["name"];
                        NewMessage._username = (string)message["author"]["username"];
                        NewMessage._role = (string)message["author"]["role"];
                        NewMessage._time = message["timestamp"].ToString().Replace("+0000", "");
                        NewMessage._status = (string)message["author"]["status"];
                        NewMessage._platform = (string)message["author"]["device"];
                        NewMessage._content = (string)message["content"];
                        NewMessage._avatar = LoadImage((string)message["author"]["avatar"]["chat"]);

                        NewMessage.c_name.Text = NewMessage.SetName;
                        NewMessage.c_user.Text = "@" + NewMessage.SetUsername;
                        NewMessage.c_role.Text = NewMessage.SetRole;
                        NewMessage.c_time.Text = NewMessage.SetTimestamp;
                        NewMessage.c_status.FillColor = Color.FromName(NewMessage.SetStatus);
                        NewMessage.c_platform.Image = null;
                        NewMessage.c_content.Text = NewMessage.SetContent;
                        NewMessage.c_avatar.Image = NewMessage.SetAvatar;

                        if (NewMessage.c_role.Text == "normal")
                        {
                            NewMessage.c_role.Text = "";
                            NewMessage.c_role.BorderStyle = BorderStyle.None;
                        }
                        else
                        {
                            NewMessage.c_role.BorderStyle = BorderStyle.FixedSingle;
                        }

                        int ListCount;
                        if (CloudMessages.Controls.Count == 0) { ListCount = 0; } else { ListCount = CloudMessages.Controls.Count - 1; }
                        CloudMessages.Controls.Add(NewMessage);
                        //use Cloudmessages.ControlAdded and change the index 

                    }));
                }
            }
        }
        private async Task PreloadMessages(ICollection<JToken> clouds)
        {
            foreach (var cloud in clouds)
            {
                this.Text = "Loading messages from " + (string)cloud["name"] + "...";
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
                        

                        CloudList.SmallImageList.Images.Add((string)cloud["id"], LoadImage((string)cloud["avatar"]["normal"]));
                        CloudList.Items.Add((string)cloud["id"], (string)cloud["name"], CloudList.SmallImageList.Images.Count - 1);        
                    }
                }
            }
        }
        private Image LoadImage(string url)
        {
            System.Net.WebRequest request =
            System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);

            responseStream.Dispose();

            return bmp;
        }
        private void LaunchReg(object sender, EventArgs e)
        {
            Process.Start("https://www.cloudsdale.org/register");
        }
        
        #region Visual changes
        private void ActivateMenuHover(object sender, EventArgs e)
        {
            if (SettingsPanel.Height != 55)
            {
                ViewTimer.Start();
            }
            else
            {
                SettingsPanel.Height = 0;
            }
        }
        private void SettingsLeft(object sender, EventArgs e)
        {
            if (SettingsVisible == false) { SettingsPanel.Height = 0; }
        }
        private void SettingsHover(object sender, EventArgs e)
        {
            SettingsVisible = true;
            SettingsPanel.Height = 55;
        }
        private void ShowSettings(object sender, EventArgs e)
        {
            var SettingsNewSize = SettingsPanel.Height;
            if (SettingsNewSize >= 55)
            {
                SettingsPanel.Height = 55;
                ViewTimer.Stop();
            }
            else
            {
                SettingsNewSize += 1 * ((SettingsNewSize + 2) / 2);
                SettingsPanel.Height = SettingsNewSize;
            }
        }
        private void CloudList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CloudList.SelectedItems.Count > 0)
            {
                if (CloudList.FocusedItem.Index >= 0){
                    RedirectHome(false);

                    MessageGroup.Text = CloudList.FocusedItem.Text;
                    this.Text = CloudList.FocusedItem.Text;
                    CurrentCloud = CloudList.FocusedItem.Name;
                    if (CloudList.FocusedItem.Index == 1)
                    {
                        CloudMessages.Visible = true;
                        CloudMessages.BringToFront();
                    }
                    else
                    {
                        CloudMessages.Visible = false;
                    }
                    
                }else{
                    RedirectHome(true);
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageGroup.Text = "Welcome back, " + User["user"]["name"] + "!";
            RedirectHome(true);
        }
        private void RedirectHome(bool RedirectedHome)
        {
            if (RedirectedHome == true)
            {
                //add the component here to release all MessageViews
                h_panel.Visible = true;

                m_NewMessage.Visible = false;
                m_SendMessage.Visible = false;

                this.Text = "Home";
            }
            else
            {
                h_panel.Visible = false;

                m_NewMessage.Visible = true;
                m_SendMessage.Visible = true;
            }
        }
        private void ResizeCheck(object sender, EventArgs e)
        {
            if (this.Width < 695) { h_about.Visible = false; }
            else { h_about.Visible = true; }
        }
        #endregion

        private void CloudContext_Opening(object sender, CancelEventArgs e)
        {
            //Add detection if user is the owner of the cloud.
        }
    }
}

