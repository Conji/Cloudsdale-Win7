using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Flyouts;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {
        public static Main Instance;
        public static CloudView CurrentView { get; set; }

        public Main()
        {
            Instance = this;
            InitializeComponent();
            SelfName.Text = App.Connection.SessionController.CurrentSession.Name;
            SelfAvatar.Source = new BitmapImage(App.Connection.SessionController.CurrentSession.Avatar.Preview);
            Clouds.ItemsSource = App.Connection.SessionController.CurrentSession.Clouds;
            Frame.Navigate(new Home());
            InitializeConnection();

            
        }

        private static void InitializeConnection()
        {
            Connection.Initialize();
        }

        private void ToggleMenu(object sender, MouseButtonEventArgs e)
        {
            ShowFlyoutMenu(new Settings());
        }

        public static void ScrollChat()
        {
            if (CurrentView != null)
            {
                CurrentView.ChatScroll.ScrollToBottom();
            }
        }

        public void ShowFlyoutMenu(Page view)
        {
            FlyoutFrame.Navigate(view);

            var board = new Storyboard();
            var animation = (FlyoutFrame.Width > 0
                                 ? new DoubleAnimation(FlyoutFrame.Width, 0.0, new Duration(new TimeSpan(2000000)))
                                 : new DoubleAnimation(FlyoutFrame.Width, 250.0, new Duration(new TimeSpan(2000000))));
            board.Children.Add(animation);
            animation.EasingFunction = new ExponentialEase();
            Storyboard.SetTargetName(animation, FlyoutFrame.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath(WidthProperty));
            
            board.Begin(this);
        }

        public void HideFlyoutMenu()
        {
            var a = new DoubleAnimation(FlyoutFrame.Width, 0.0, new Duration(new TimeSpan(2000000)));
            a.EasingFunction = new ExponentialEase();
            FlyoutFrame.BeginAnimation(WidthProperty, a);
        }

        private void CloudsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Clouds.SelectedIndex == -1)
            {
                Frame.Navigate(new Home());
                return;
            }
            var cloud = (ListView)sender;
            var item = (Cloud)cloud.SelectedItem;
            App.Connection.MessageController.CurrentCloud = App.Connection.MessageController[item];
            var cloudView = new CloudView(item);
            Frame.Navigate(cloudView);
            CurrentView = cloudView;
            HideFlyoutMenu();
        }

        private void DirectHome(object sender, MouseButtonEventArgs e)
        {
            Frame.Navigate(new Home());
            Clouds.SelectedIndex = -1;
        }
        public void NavigateToCloud(CloudController cloud)
        {
            Frame.Navigate(new CloudView(cloud.Cloud));

        }

        private void LaunchExplore(object sender, RoutedEventArgs e)
        {
            Clouds.SelectedIndex = -1;
            Frame.Navigate(new Explore());
        }

        #region Cloud Reorder Mapping



        #endregion

        #region Notify

        public void Notify(Message message)
        {

            var post = App.Connection.MessageController.CloudControllers[message.PostedOn];
            if (App.Connection.MessageController.CurrentCloud == post) return;

            NoteTitle.Text = "@" + message.Author.Username + "(" + post.Cloud.Name + "):";
            NoteContent.Text = message.Content;
            ShowNote();
            HideNote();
        }

        private void ShowNote()
        {
            var a = new DoubleAnimation(0.0, 100.0, new Duration(new TimeSpan(0, 0, 2)));
            a.EasingFunction = new ExponentialEase();
            Note.BeginAnimation(OpacityProperty, a);
        }

        private void HideNote()
        {
            var a = new DoubleAnimation(100.0, 0.0, new Duration(new TimeSpan(0, 0, 6)));
            a.EasingFunction = new ExponentialEase();
            Note.BeginAnimation(OpacityProperty, a);
        }

        #endregion

        private async void CreateNewCloud(object sender, RoutedEventArgs e)
        {
            var reg = new Regex(@"^[a-z_]+$", RegexOptions.IgnoreCase);
            if (NewCloudName.Opacity.Equals(0.0))
            {
                NewCloudName.BeginAnimation(OpacityProperty, new DoubleAnimation(NewCloudName.Opacity, 100.0, new Duration(new TimeSpan(0, 0, 3))));
                return;
            }
            if (String.IsNullOrWhiteSpace(NewCloudName.Text))
            {
                NewCloudName.BeginAnimation(OpacityProperty, new DoubleAnimation(NewCloudName.Opacity, 0.0, new Duration(new TimeSpan(0,0,2))));
                return;
            }
            
            if (!reg.IsMatch(NewCloudName.Text))
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message
                                                                              {
                                                                                  Content =
                                                                                      "Cloud name can only contain numbers, letters, and underscores!"
                                                                              });
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
            var response = await client.PostAsync("http://www.cloudsdale.org/v1/clouds",new JsonContent(data));

            var cloud = await JsonConvert.DeserializeObjectAsync<WebResponse<Cloud>>(await response.Content.ReadAsStringAsync());
            if (cloud.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client, new Message{Content = cloud.Flash.Message});
                return;
            }
            App.Connection.SessionController.CurrentSession.Clouds.Add(cloud.Result);
            App.Connection.SessionController.RefreshClouds();
            FayeConnector.Subscribe("/clouds/" + cloud.Result.Id + "/chat/messages");
            Clouds.SelectedItem = cloud;
            NewCloudName.Opacity = 0.0;
        }
    }
}
