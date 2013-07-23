using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Cloudsdale_Win7.Assets;
using Cloudsdale_Win7.Cloudsdale;
using Cloudsdale_Win7.MVVM;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7 {
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView {
        public JToken Cloud;

        public CloudView(JToken cloud) {
            Cloud = cloud;
            InitializeComponent();
            ChatMessages.Items.Clear();
            Title = (string)cloud["name"];
            DataContext = cloud;
            MessageSource.GetSource(Cloud).Messages.CollectionChanged += NewMessage;
            ChatMessages.ItemsSource = MessageSource.GetSource(Cloud).Messages;
            MaxCharContainer.DataContext = MainWindow.Instance;
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
            ((DependencyJToken)Resources["Cloud"]).Token = cloud;
        }

        ~CloudView() {
            MessageSource.GetSource(Cloud).Messages.CollectionChanged -= NewMessage;
        }

        private void NewMessage(object sender, EventArgs e) {
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
        }

        private void SendBoxEnter(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            if (string.IsNullOrWhiteSpace(InputBox.Text)) return;
            Send(InputBox.Text, (string)Cloud["id"]);
            InputBox.Text = "";

        }

        internal void Send(string message, string cloudId) {
            var dataObject = new JObject();
            dataObject["content"] = message;
            dataObject["client_id"] = FayeConnector.ClientID;
            dataObject["device"] = "desktop";
            var data = Encoding.UTF8.GetBytes(dataObject.ToString());
            var request = WebRequest.CreateHttp(Endpoints.CloudMessages.Replace("[:id]", cloudId));
            request.Accept = "application/json";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = MainWindow.User["user"]["auth_token"].ToString();
            request.BeginGetRequestStream(ar => {
                var reqs = request.EndGetRequestStream(ar);
                reqs.Write(data, 0, data.Length);
                reqs.Close();
                request.BeginGetResponse(a => {
                    try {
                        var response = request.EndGetResponse(a);
                        response.Close();
                    } catch (Exception ex) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }

        private void DropUp(object sender, MouseButtonEventArgs e) {
            var drop = (JToken)((FrameworkElement)sender).DataContext;
            Process.Start((string)drop["url"]);
        }
    }
}
