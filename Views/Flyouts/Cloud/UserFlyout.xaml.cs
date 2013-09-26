using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Flyouts.Cloud
{
    /// <summary>
    /// Interaction logic for UserFlyout.xaml
    /// </summary>
    public partial class UserFlyout
    {

        private static User Self { get; set; }
        private static lib.Models.Cloud FoundOn { get; set; }

        public UserFlyout(User user, lib.Models.Cloud cloud)
        {
            InitializeComponent();
            Self = user;
            FoundOn = cloud;
            AvatarBounce();
            Username.Text = "@" + user.Username;
            Name.Text = user.Name;
            AviImage.Source = new BitmapImage(user.Avatar.Normal);
            
        }

        private void AvatarBounce()
        {
            var a = new ThicknessAnimation(new Thickness(10, -800, 0, 810), new Thickness(10, 10, 0, 0),
                                           new Duration(new TimeSpan(0, 0, 1)));
            var bounce = new BounceEase();
            bounce.Bounces = 3;
            bounce.Bounciness = 10;
            a.EasingFunction = bounce;
            Avi.BeginAnimation(MarginProperty, a);
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            Main.Instance.ShowFlyoutMenu(this);
        }

        private void AddOnSkype(object sender, RoutedEventArgs e)
        {
            UIHelpers.MessageOnSkype(Self.SkypeName ?? Self.SkypeName);
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
