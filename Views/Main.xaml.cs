using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using CloudsdaleWin7.lib.CloudsdaleLib.Misc;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.Views.CloudViews;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Settings = CloudsdaleWin7.Views.Flyouts.Settings;

namespace CloudsdaleWin7.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {
        public static Main Instance;
        public static CloudView CurrentView = CloudView.Instance;

        public Main()
        {
            Instance = this;
            OnFourOne.NexLogin();
            InitializeComponent();
            InitSession();
            Clouds.ItemsSource = App.Connection.SessionController.CurrentSession.Clouds;
            Frame.Navigate(new Home());
            Connection.Initialize();
            CreateCloud.Visibility = !CanMakeCloud ? Visibility.Hidden : Visibility.Visible;

        }

        public void InitSession()
        {
            SelfAvatar.Source = new BitmapImage(App.Connection.SessionController.CurrentSession.Avatar.Normal);
            SelfName.Text = App.Connection.SessionController.CurrentSession.Name;
        }

        private void ToggleMenu(object sender, MouseButtonEventArgs e)
        {
            ShowFlyoutMenu(new Settings());
        }

        public void ShowFlyoutMenu(Page view)
        {
            if (FlyoutFrame.Width > 0)
            {
                HideFlyoutMenu();
                return;
            }
            FlyoutFrame.Navigate(view);
            if (!FlyoutFrame.Width.Equals(250))
                FlyoutFrame.BeginAnimation(WidthProperty, new DoubleAnimation(FlyoutFrame.Width, 250.0, new Duration(new TimeSpan(2000000)))
                                 {EasingFunction = new ExponentialEase()});
        }

        public void HideFlyoutMenu()
        {
            FlyoutFrame.BeginAnimation(WidthProperty, new DoubleAnimation(FlyoutFrame.Width, 0.0, new Duration(new TimeSpan(2000000))) { EasingFunction = new ExponentialEase() });
        }

        private async void CloudsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Clouds.SelectedIndex == -1)
            {
                Frame.Navigate(new Home());
                App.Connection.MessageController.HasCloudSelected = false; 
                return;
            }
            App.Connection.MessageController.HasCloudSelected = true;
            var cloud = (ListView)sender;
            var item = (Cloud)cloud.SelectedItem;
            App.Connection.MessageController.CurrentCloud = App.Connection.MessageController[item];
            
            Dispatcher.InvokeAsync(async () =>
                                   {
                                       Frame.Navigate(new LoadingView());
                                       await App.Connection.MessageController.CurrentCloud.LoadBans();
                                       foreach (var ban in App.Connection.MessageController.CurrentCloud.Bans.Where(b => (b.OffenderId == App.Connection.SessionController.CurrentSession.Id)).Where(ban => ban.Active == true))
                                       {
                                           Clouds.SelectedIndex = -1;
                                           Frame.Navigate(new BannedCloud(ban, item.Id));
                                           return;
                                       }
                                       await App.Connection.MessageController[item].LoadMessages(false);
                                       Frame.Navigate(new CloudView(item));
                                       await App.Connection.MessageController.CurrentCloud.LoadModerators();
                                       await App.Connection.MessageController.CurrentCloud.LoadUsers();
                                   }, DispatcherPriority.Background);
            HideFlyoutMenu();
            CreateCloud.Visibility = !CanMakeCloud ? Visibility.Hidden : Visibility.Visible;
        }

        private void DirectHome(object sender, MouseButtonEventArgs e)
        {
            Frame.Navigate(new Home());
            Clouds.SelectedIndex = -1;
        }

        private static bool CanMakeCloud
        {
            get
            {
                return App.Connection.SessionController.CurrentSession.IsStaff || App.Connection.SessionController.CurrentSession.Clouds.All(i => i.OwnerId != App.Connection.SessionController.CurrentSession.Id);
            }
        }

        private void LaunchExplore(object sender, RoutedEventArgs e)
        {
            Clouds.SelectedIndex = -1;
            Frame.Navigate(new Explore());
        }


        private async void CreateNewCloud(object sender, RoutedEventArgs e)
        {
            var reg = new Regex(@"^[a-z0-9_]+$", RegexOptions.IgnoreCase);
            if (NewCloudName.Visibility == Visibility.Hidden)
            {
                NewCloudName.Visibility = Visibility.Visible;
                return;
            }
            if (String.IsNullOrWhiteSpace(NewCloudName.Text))
            {
                NewCloudName.Visibility = Visibility.Hidden;
                return;
            }
            
            if (!reg.IsMatch(NewCloudName.Text))
            {
                App.Connection.NotificationController.Notification.Notify("Cloud name can only contain numbers, letters, and underscores!");
                return;
            }

            var client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    {"Accept", "application/json"},
                    {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken},
                }
            };
            var data = JObject.FromObject(new
            {
                cloud = new
                {
                    name = NewCloudName.Text,
                    short_name = NewCloudName.Text.Trim().ToLower()
                }
            }).ToString();
            var response = await client.PostAsync("http://www.cloudsdale.org/v1/clouds", new JsonContent(data));

            var cloud = await JsonConvert.DeserializeObjectAsync<WebResponse<Cloud>>(await response.Content.ReadAsStringAsync());
            if (cloud.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(cloud.Flash.Message);
                return;
            }
            App.Connection.SessionController.CurrentSession.Clouds.Add(cloud.Result);
            App.Connection.SessionController.RefreshClouds();
            FayeConnector.Subscribe("/clouds/" + cloud.Result.Id + "/chat/messages");
            Clouds.SelectedIndex = Clouds.Items.Count - 1;
            NewCloudName.Visibility = Visibility.Hidden;
            NewCloudName.Text = "";
            if (!CanMakeCloud) CreateCloud.Visibility = Visibility.Hidden;
        }

        private async void LeaveCloud(object sender, RoutedEventArgs e)
        {
            var c = (Cloud)((Button)sender).DataContext;
            if (c.OwnerId == App.Connection.SessionController.CurrentSession.Id)
            {
                if (MessageBox.Show("Are you sure you want to delete this cloud?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    return;
                var client = new HttpClient
                             {
                                 DefaultRequestHeaders =
                                 {
                                     {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken }
                                 }
                             }.AcceptsJson();
                await client.DeleteAsync(Endpoints.Cloud.Replace("[:id]", c.Id));
                App.Connection.SessionController.CurrentSession.Clouds.Remove(c);
                App.Connection.SessionController.RefreshClouds();
                HideFlyoutMenu();
                return;
            }

            if (MessageBox.Show("Are you sure you want to leave this cloud?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            
            c.Leave();
            if (!CanMakeCloud) CreateCloud.Visibility = Visibility.Hidden;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            CreateNewCloud(this, new RoutedEventArgs());
        }

        private void CheckShortcuts(object sender, KeyEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;
            switch (e.Key)
            {
                case Key.S:
                    if (Clouds.SelectedIndex == Clouds.Items.Count) return;
                    Clouds.SelectedIndex += 1;
                    break;
                case Key.W:
                    if (Clouds.SelectedIndex == 0) return;
                    Clouds.SelectedIndex -= 1;
                    break;
                case Key.E:
                    Frame.Navigate(new Explore());
                    break;
                case Key.H:
                    Frame.Navigate(new Home());
                    break;
                case Key.Space:
                    CloudView.Instance.CaptureChat(this, null);
                    break;
            }
        }

        private void ChangeTitle(object sender, NavigationEventArgs e)
        {
            MainWindow.Instance.Title = ((Page) e.Content).Title;
        }
    }
}
