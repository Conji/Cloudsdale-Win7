using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView {
        public JToken Cloud;
        public static CloudView Instance;

        public CloudView(JToken cloud)
        {
            Instance = this;
            Cloud = cloud;
            InitializeComponent();
            //clear items
            Title = (string)cloud["name"];
            DataContext = cloud;
            MessageSource.GetSource(Cloud).Messages.CollectionChanged += NewMessage;
            //set new source
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
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

        internal void Send(string message, string cloudId)
        {
            var dataObject = new JObject();
            dataObject["content"] = message;
            dataObject["client_id"] = App.Connection.Faye.ClientId;
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
            MainWindow.Instance.MainFrame.Navigate(new Browser());
            Main.Instance.Clouds.SelectedIndex = -1;
            Browser.Instance.WebBrowser.Navigate((string)drop["url"]);
            Browser.Instance.WebAddress.Text = ((string) drop["url"]);
            
            Browser.Instance.BrowserPage.Width = MainWindow.Instance.Width;
        }

        private void ShowUserInfo(object sender, EventArgs e)
        {
            var block = (TextBlock) sender;
            var j = (JObject) block.DataContext;
            var user = new UserInfo();
            user.ShowUserInfo((JObject) j["author"]);
            if (MainWindow.CurrentCloud["moderator_ids"].ToString().Contains(MainWindow.User["user"]["id"].ToString()) || MainWindow.CurrentCloud["owner_id"].ToString() == MainWindow.User["user"]["id"].ToString())
            {
                UserInfo.Instance.ModeratorTools.Visibility = Visibility.Visible;
            }
            else
            {
                UserInfo.Instance.ModeratorTools.Visibility = Visibility.Collapsed;
            }
        }

        private void Quote(object sender, RoutedEventArgs e)
        {
            var obj = (MenuItem) sender;
            var text = obj.DataContext.ToString();
            InputBox.Text = "> " + text.Replace(@"\n", Environment.NewLine + "> ");
            
        }

        private void CheckIfTextIsMultiLine(object sender, TextChangedEventArgs e)
        {
            InputBox.Text.Replace(@"\n", Environment.NewLine);
            if (InputBox.LineCount != 1)
            {
                InputBox.MaxHeight *= 4;
                InputBox.Height *= InputBox.LineCount;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SymbolBox.Visibility == Visibility.Visible)
            {
                SymbolBox.Visibility = Visibility.Hidden;
            }else
            {
                SymbolBox.Visibility = Visibility.Visible;
            }
        }
        private void ShowCloudInfo(object sender, MouseButtonEventArgs args)
        {
            
        }
    }
}
