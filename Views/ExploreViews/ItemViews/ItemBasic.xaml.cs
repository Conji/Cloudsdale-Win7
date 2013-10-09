using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.ExploreViews.ItemViews
{
    /// <summary>
    /// Interaction logic for ItemBasic.xaml
    /// </summary>
    public partial class ItemBasic
    {
        private Cloud Cloud { get; set; }

        public ItemBasic(Cloud cloud)
        {
            InitializeComponent();
            BasicAvatar.Source = new BitmapImage(cloud.Avatar.Normal);
            BasicName.Text = cloud.Name;
            Cloud = cloud;
        }

        private void ShowHiddenUi(object sender, MouseEventArgs e)
        {
            var a = new ThicknessAnimation(new Thickness(0,-38,0,39), new Thickness(0,0,0,39), new Duration(new TimeSpan(2000000)));
            a.EasingFunction = new ExponentialEase();
            BackUI.BeginAnimation(MarginProperty, a);
        }

        private void HideHiddenUi(object sender, MouseEventArgs e)
        {
            var a = new ThicknessAnimation(new Thickness(0, 0, 0, 39), new Thickness(0, -38, 0, 39),
                                           new Duration(new TimeSpan(2000000)));
            BackUI.BeginAnimation(MarginProperty, a);
        }

        private async void Join(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient().AcceptsJson();
            var response = await client.GetStringAsync(Endpoints.Cloud.Replace("[:id]", Cloud.Id));
            var responseObject = await JsonConvert.DeserializeObjectAsync<WebResponse<Cloud>>(response);
            if (responseObject.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {Content = responseObject.Flash.Message}
                    );
                return;
            }

            BrowserHelper.JoinCloud(responseObject.Result);
        }
    }
}
