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

namespace Cloudsdale_Win7
{
    /// <summary>
    /// Interaction logic for Browser.xaml
    /// </summary>
    public partial class Browser : Page
    {
        public static Browser Instance;
        public Browser()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!WebAddress.Text.StartsWith("http://"))
            {
                WebBrowser.Navigate("http://" + WebAddress.Text);
            }else{ WebBrowser.Navigate(WebAddress.Text);}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WebBrowser.GoBack();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WebBrowser.GoForward();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Frame.Navigate(new Home());
        }
    }
}
