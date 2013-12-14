using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Controls
{
    /// <summary>
    /// Interaction logic for StandMessageView.xaml
    /// </summary>
    public partial class StandardMessageView
    {
        public StandardMessageView()
        {
            InitializeComponent();
        }

        private void Mention(object sender, MouseButtonEventArgs e)
        {
            Main.CurrentView.InputBox.Text = "@" + ((Run) sender).Text + " ";
            Main.CurrentView.InputBox.Focus();
        }

        private async void UserInfo(object sender, MouseButtonEventArgs e)
        {
            var user = ((User) ((Run) sender).DataContext);

            var client = new HttpClient().AcceptsJson();
            var response =
                await JsonConvert.DeserializeObjectAsync<WebResponse<User>>(
                    client.GetStringAsync(Endpoints.User.Replace("[:id]", user.Id)).Result);

            if (response.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(response.Flash.Message);
                return;
            }

            Main.Instance.ShowFlyoutMenu(new UserFlyout(response.Result));
        }

        private void Quote(object sender, RoutedEventArgs e)
        {
            CloudView.Instance.InputBox.Text = "> " + ((MenuItem) sender).DataContext.ToString().Replace("\r\n", "\r\n> ");
            CloudView.Instance.InputBox.Focus();
        }

        private void DeleteMessage(object sender, MouseButtonEventArgs e)
        {
            var client = new HttpClient().AcceptsJson();
            var response =
                client.DeleteAsync(Endpoints.CloudMessages.Replace("[:id]",
                                                           App.Connection.MessageController.CurrentCloud.Cloud.Id));
            Console.WriteLine(response.Result.Content.ReadAsStringAsync().Result);
        }
        
    }
}
