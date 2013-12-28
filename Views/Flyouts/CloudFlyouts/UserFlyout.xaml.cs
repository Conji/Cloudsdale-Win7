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
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.Flyouts.CloudFlyouts
{
    /// <summary>
    /// Interaction logic for UserFlyout.xaml
    /// </summary>
    public partial class UserFlyout
    {

        private static User Self { get; set; }
        public static UserFlyout Instance;

        public UserFlyout(User user)
        {
            InitializeComponent();
            Instance = this;
            Self = user;
            AdminUI.Visibility = App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.Contains(App.Connection.SessionController.CurrentSession.Id)
                && Self.Role != "founder"
                ? Visibility.Visible : Visibility.Hidden;
            InitializeUser();
        }

        private async void InitializeUser()
        {
            AvatarBounce();
            await App.Connection.MessageController[App.Connection.MessageController.CurrentCloud.Cloud].LoadBans();
            Username.Text = "@" + Self.Username;
            Name.Text = Self.Name;
            AviImage.Source = new BitmapImage(Self.Avatar.Normal);
            SkypeUI.Visibility = Self.SkypeName != null ? Visibility.Visible : Visibility.Hidden;
            PromoteCommand.Visibility = App.Connection.MessageController.CurrentCloud.Cloud.OwnerId == App.Connection.SessionController.CurrentSession.Id
                                            ? Visibility.Visible
                                            : Visibility.Hidden;
            if (App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.Contains(Self.Id)) PromoteCommand.Content = "Demote Moderator";
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
            foreach (var ban in App.Connection.MessageController[App.Connection.MessageController.CurrentCloud.Cloud].BansByUser.Where(ban => ban.Key == Self.Id))
            {
                list.Add(ban.Value);
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

            if (MessageBox.Show("Ban @" + Self.Username + " for \"" + BanReason.Text + "\" until " + BanCal.SelectedDate + "?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
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
            var response = await client.PostAsync(
                    Endpoints.CloudBan.Replace("[:id]", App.Connection.MessageController.CurrentCloud.Cloud.Id), new JsonContent(ban));

            var responseObject =
                JsonConvert.DeserializeObjectAsync<WebResponse<Ban>>(response.Content.ReadAsStringAsync().Result);
            if (responseObject.Result.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(responseObject.Result.Flash.Message);
                return;
            }
            App.Connection.MessageController[App.Connection.MessageController.CurrentCloud.Cloud].Bans.Add(responseObject.Result.Result);
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
                    var response = client.PostAsync(Endpoints.CloudModerators, new JsonContent(Self.Id));
                    Console.WriteLine(response);
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
            
            if (ban.Active == false)
            {
                MessageBox.Show("This ban is no longer active!");
                return;
            }
            ban.Revoke();
            Main.Instance.ShowFlyoutMenu(this);
        }
    }
}
