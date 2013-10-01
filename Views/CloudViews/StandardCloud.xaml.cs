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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.CloudViews
{
    /// <summary>
    /// Interaction logic for StandardCloud.xaml
    /// </summary>
    public partial class StandardCloud : Page
    {
        public StandardCloud(Cloud cloud)
        {
            InitializeComponent();
            CloudAvatar.Source = new BitmapImage(cloud.Avatar.Normal);
            CloudName.Text = cloud.Name;
            Shortlink.Text = "/clouds/" + cloud.ShortName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }
    }
}
