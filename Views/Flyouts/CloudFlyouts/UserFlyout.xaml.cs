using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Flyouts.CloudFlyouts
{
    /// <summary>
    /// Interaction logic for UserFlyout.xaml
    /// </summary>
    public partial class UserFlyout
    {

        private static User Self { get; set; }
        private static Cloud FoundOn { get; set; }

        public UserFlyout(User user, Cloud cloud, bool isMod)
        {
            InitializeComponent();
            FoundOn = cloud;
            user.ForceValidate();
            Self = user;
            AvatarBounce();
            Username.Text = "@" + user.Username;
            Name.Text = user.Name;
            AviImage.Source = new BitmapImage(user.Avatar.Normal);
            SkypeUI.Visibility = user.SkypeName != null ? Visibility.Visible : Visibility.Hidden;
            akaList.ItemsSource = user.AlsoKnownAs;
            BanReason.Text = "Banned by @" + App.Connection.SessionController.CurrentSession.Username;
            AdminUI.Visibility = isMod ? Visibility.Visible : Visibility.Hidden;
            PreviousBans.ItemsSource = UnexpiredBans();

            BanCal.SelectedDate = DateTime.Now;
        }

        private void AvatarBounce()
        {

            var bounce = new BounceEase { Bounces = 3, Bounciness = 10 };
            var a = new ThicknessAnimation(new Thickness(10, -800, 0, 810), new Thickness(10, 10, 0, 0),
                                           new Duration(new TimeSpan(0, 0, 1))) {EasingFunction = bounce};
            Avi.BeginAnimation(MarginProperty, a);
        }

        private void AddOnSkype(object sender, RoutedEventArgs e)
        {
            UiHelpers.MessageOnSkype(Self.SkypeName ?? Self.SkypeName);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }

        private void ShowBanUi(object sender, RoutedEventArgs e)
        {
            BanUI.Visibility = BanUI.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private static IEnumerable<Ban> UnexpiredBans()
        {
            var list = new ObservableCollection<Ban>();
            foreach (var ban in App.Connection.MessageController[FoundOn].Bans.Where(ban => (ban.Expired == false
                                                                                            || ban.Revoked == false)
                                                                                            && ban.OffenderId == Self.Id))
            {
                list.Add(ban);
            }
            return list;
        }

        private async void AttemptBan(object sender, RoutedEventArgs e)
        {
            if (BanCal.SelectedDate == null)
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {Content = "You have to select a date!"});
                return;
            }
            
            if (String.IsNullOrEmpty(BanReason.Text))
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {
                                                                                  Content = "Can't leave the reason blank!"
                                                                              });
                return;
            }

            if (MessageBox.Show("Ban @" + Self.Username + " for " + BanReason.Text + " until " + BanCal.SelectedDate + "?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var ban = new Ban(Self.Id)
                          {
                              OffenderId = Self.Id,
                              Reason = BanReason.Text,
                              Due = BanCal.SelectedDate
                          };
            var client = new HttpClient
                             {
                                 DefaultRequestHeaders =
                                     {
                                         {"Accept", "application/json"},
                                         {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                                     }
                             };
            await client.PostAsync(
                    Endpoints.CloudBan.Replace("[:id]", App.Connection.MessageController.CurrentCloud.Cloud.Id), new JsonContent(ban));

        }
    }
}
