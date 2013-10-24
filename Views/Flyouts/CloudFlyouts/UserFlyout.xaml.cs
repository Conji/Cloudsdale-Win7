using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        public static UserFlyout Instance;

        public UserFlyout(User user, Cloud cloud, bool isMod)
        {
            InitializeComponent();
            Instance = this;
            FoundOn = cloud;
            Self = user;
            AdminUI.Visibility = isMod ? Visibility.Visible : Visibility.Hidden;
            InitializeUser();
        }

        private void InitializeUser()
        {
            AvatarBounce();
            Username.Text = "@" + Self.Username;
            Name.Text = Self.Name;
            AviImage.Source = new BitmapImage(Self.Avatar.Normal);
            SkypeUI.Visibility = Self.SkypeName != null ? Visibility.Visible : Visibility.Hidden;
            PromoteCommand.Visibility = FoundOn.OwnerId == App.Connection.SessionController.CurrentSession.Id
                                            ? Visibility.Visible
                                            : Visibility.Hidden;
            if (FoundOn.ModeratorIds.Contains(Self.Id)) PromoteCommand.Content = "Demote Moderator";
            akaList.ItemsSource = Self.AlsoKnownAs;
            BanReason.Text = "Banned by @" + App.Connection.SessionController.CurrentSession.Username;
            
            PreviousBans.ItemsSource = UnexpiredBans().Reverse();

            BanCal.SelectedDate = DateTime.Now.AddDays(1.0);
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

        public static IEnumerable<Ban> UnexpiredBans()
        {
            var list = new ObservableCollection<Ban>();
            //foreach (var ban in App.Connection.MessageController[FoundOn].Bans.Where(ban => (ban.Expired == false
            //                                                                                || ban.Revoked == false)
            //                                                                                && ban.OffenderId == Self.Id))
            //{
            //    list.Add(ban);
            //}
            foreach (var ban in App.Connection.MessageController[FoundOn].Bans.Where(ban => ban.OffenderId == Self.Id))
            {
                list.Add(ban);
            }
            return list;
        }

        private async void AttemptBan(object sender, RoutedEventArgs e)
        {
            if (BanCal.SelectedDate == null)
            {
                App.Connection.NotificationController.Notification.Notify("You have to select a date!");
                return;
            }
            
            if (String.IsNullOrEmpty(BanReason.Text))
            {
                App.Connection.NotificationController.Notification.Notify("You can't leave the reason blank!");
                return;
            }

            if (MessageBox.Show("Ban @" + Self.Username + " for " + BanReason.Text + " until " + BanCal.SelectedDate + "?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var ban = new Ban(Self.Id)
                          {
                              OffenderId = Self.Id,
                              Reason = BanReason.Text,
                              Due = BanCal.SelectedDate.Value.ToUniversalTime()
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
            
            Main.Instance.ShowFlyoutMenu(this);
        }

        private void AdjustModeration(object sender, RoutedEventArgs e)
        {
            var cmd = (Button) sender;

            var client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                     {"Accept", "application/json"},
                     {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                }
            };

            switch (cmd.Content.ToString())
            {
                case "Promote To Moderator":
                    break;
                case "Demote Moderator":
                    break;
            }
        }

        private void AttemptRevoke(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var ban = (Ban) ((Run) sender).DataContext;
            if (MessageBox.Show("Would you like to revoke the ban for " + ban.Offender.Name + "?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            
            if (ban.Revoked == true || ban.Expired == true)
            {
                MessageBox.Show("Ban has either been revoked or already ended!");
                return;
            }
            ban.Revoke();
            Main.Instance.ShowFlyoutMenu(this);
        }
    }
}
