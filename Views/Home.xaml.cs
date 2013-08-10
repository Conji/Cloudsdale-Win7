using System.Windows;
using System.Windows.Controls;
using Cloudsdale_Win7.Win7_Lib.ErrorConsole;

namespace Cloudsdale_Win7 {
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public static Home Instance;
        public Home() {
            InitializeComponent();
            RootGrid.DataContext = MainWindow.User;
            Instance = this;
            MainWindow.Instance.CloudList.Width = 200;
            JoinDate.Text += MainWindow.User["user"]["member_since"].ToString().Split(' ')[0];
        }

        private void ShowConsole(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var console = new ErrorConsole();
            console.Visibility = Visibility.Visible;
            console.Show();
        }
    }
}
