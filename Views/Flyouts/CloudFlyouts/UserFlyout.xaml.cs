using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            AdminUI.Visibility = IsUserBannable ? Visibility.Visible : Visibility.Hidden;
            InitializeUser();
        }

        public bool IsUserBannable
        {
            get
            {
                //checks if the flyout is for the logged in user.
                if (Self.Id == App.Connection.SessionController.CurrentSession.Id) return false;
                //checks if the viewed user is role of founder.
                if (Self.Role == "founder") return false;
                //checks if logged in user is an admin.
                if (App.Connection.SessionController.CurrentSession.Role == "admin") return true;
                //checks if the cloud is owned by logged in user.
                if (App.Connection.MessageController.CurrentCloud.Cloud.OwnerId ==
                    App.Connection.SessionController.CurrentSession.Id) return true;
                //checks if viewed user is a moderator.
                if (App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.Contains(Self.Id)) return false;
                //checks if the user is a moderator. Returns false otherwise.
                return App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.Contains(
                    App.Connection.SessionController.CurrentSession.Id);
            }
        }

        private async void InitializeUser()
        {
            AvatarBounce();
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

            PreviousBans.ItemsSource =
                App.Connection.MessageController.CurrentCloud.Bans.Where(b => b.OffenderId == Self.Id);

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

        public static async Task<ObservableCollection<Ban>> UnexpiredBans()
        {
            var list = new ObservableCollection<Ban>();
            await App.Connection.MessageController.CurrentCloud.LoadBans();
            foreach (var ban in App.Connection.MessageController.CurrentCloud.Bans.Where(b => b.OffenderId == Self.Id))
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

        private async void AdjustModeration(object sender, RoutedEventArgs e)
        {
            var id = App.Connection.MessageController.CurrentCloud.Cloud.Id;
            var client = new HttpClient
                         {
                             DefaultRequestHeaders =
                             {
                                 {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                             }
                         }.AcceptsJson();
            var content = "";

            switch (App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.Contains(Self.Id))
            {
                case false:

                    content = "{\"cloud\": { \"x_moderator_ids\": " +
                              JArray.FromObject(App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.Concat(new[] { Self.Id })) + "}}";
                    
                    break;
                case true:
                    content = "{\"cloud\": { \"x_moderator_ids\": " +
                              JArray.FromObject(App.Connection.MessageController.CurrentCloud.Cloud.ModeratorIds.ToArray()
                                  .Remove(Self.Id)) + "}}";
                    break;
            }
            Console.WriteLine(content);
            var response =
                await
                    client.PostAsync(
                        Endpoints.Cloud.Replace("[:id]", id),
                        new JsonContent(content));
            var responseObject =
                await JsonConvert.DeserializeObjectAsync<WebResponse<Cloud>>(await response.Content.ReadAsStringAsync());
            App.Connection.ModelController.Clouds[id].ModeratorIds = responseObject.Result.ModeratorIds;
        }

        private async void AttemptRevoke(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var ban = (Ban) ((Run) sender).DataContext;
            if (MessageBox.Show("Would you like to revoke the ban for " + ban.Offender.Name + "?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            
            if (ban.Active == false)
            {
                MessageBox.Show("This ban is no longer active!");
                return;
            }
            await ban.Revoke();
            Main.Instance.ShowFlyoutMenu(this);
        }
    }
}
