using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public static Main Instance;
        private static bool MenuVisible = false;

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
            if (FlyoutFrame.Visibility == Visibility.Collapsed)
            {
                FlyoutFrame.Visibility = Visibility.Visible;
            }
            else
            {
                FlyoutFrame.Visibility = Visibility.Collapsed;
            }
        }
    }
}
