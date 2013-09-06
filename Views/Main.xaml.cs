using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Flyouts;

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
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            ShowFlyoutMenu(new Settings());
        }

        public void ShowFlyoutMenu(Page view)
        {
            var board = new Storyboard();
            var animation = (FlyoutFrame.Width > 0
                                 ? new DoubleAnimation(FlyoutFrame.Width, 0.0, new Duration(new TimeSpan(2000000)))
                                 : new DoubleAnimation(0.0, 150.0, new Duration(new TimeSpan(2000000))));
            //var animation = new DoubleAnimation(0.0, 150.0, new Duration(new TimeSpan(200000000)));
            board.Children.Add(animation);
            animation.EasingFunction = new ExponentialEase();
            Storyboard.SetTargetName(animation, FlyoutFrame.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath(WidthProperty));
            FlyoutFrame.Navigate(view);
            board.Begin(this);
        }

        private void CloudsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cloud = (ListView) sender;
            var item = (lib.Models.Cloud) cloud.SelectedItem;
            Frame.Navigate(new CloudView(item));
            
        }

        private void DirectHome(object sender, MouseButtonEventArgs e)
        {
            Frame.Navigate(new Home());
        }
    }
}
