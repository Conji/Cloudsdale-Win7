using System.Windows;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.CloudViews
{
    /// <summary>
    /// Interaction logic for StandardCloud.xaml
    /// </summary>
    public partial class StandardCloud
    {

        private Cloud Cloud { get; set; }

        public StandardCloud(Cloud cloud)
        {
            InitializeComponent();
            CloudAvatar.Source = new BitmapImage(cloud.Avatar.Normal);
            CloudName.Text = cloud.Name;
            Shortlink.Text = "/clouds/" + cloud.ShortName;
            Cloud = cloud;
            if (cloud.Rules != null) Rules.Text = cloud.Rules.Replace("\n", "\r\n");
            if (cloud.Description != null) Description.Text = cloud.Description.Replace("\n", "\r\n");
            if (Cloud.IsSubscribed) SubBox.IsChecked = true;
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }

        private void Leave(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave this cloud?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Cloud.Leave();
        }

        private void Subscribe(object sender, RoutedEventArgs e)
        {
            App.Connection.MessageController[Cloud].Cloud.IsSubscribed = true;
        }

        private void Unsubscribe(object sender, RoutedEventArgs e)
        {
            App.Connection.MessageController[Cloud].Cloud.IsSubscribed = false;
        }
    }
}
