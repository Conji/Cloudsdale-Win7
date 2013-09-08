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

namespace CloudsdaleWin7.Views.Initial
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : Page
    {
        public Confirm()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Waiting.Visibility = Visibility.Visible;
            App.Connection.SessionController.CurrentSession.HasReadTnc = true;
            if (App.Connection.SessionController.CurrentSession.Validate().Result == false)
            {
                Waiting.Visibility = Visibility.Hidden;
                MessageBox.Show("We're sorry! An error occured when trying to process your request.");
            }else
            {
                MainWindow.Instance.MainFrame.Navigate(new Main());
            }
        }
    }
}
