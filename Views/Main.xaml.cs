using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Flyouts;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Faye;

namespace CloudsdaleWin7.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {
        public static Main Instance;

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

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            ShowFlyoutMenu(new Settings());
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
            if (FlyoutFrame.Content.Equals(view)) return;
            
            board.Begin(this);
        }

        private void CloudsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Clouds.SelectedIndex == -1) return;
            var cloud = (ListView)sender;
            var item = (lib.Models.Cloud)cloud.SelectedItem;
            Frame.Navigate(new CloudView(item));
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
    }
}
