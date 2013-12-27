using System.Net.Http;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Controls
{
    /// <summary>
    /// Interaction logic for ActionMessageView.xaml
    /// </summary>
    public sealed partial class ActionMessageView
    {
        public ActionMessageView()
        {
            InitializeComponent();
        }

        private void ActionMessageView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Separator.Width = e.NewSize.Width;
        }

        private async void UserInfo(object sender, MouseButtonEventArgs e)
        {
            var user = ((User)((Run)sender).DataContext);

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
    }
}
