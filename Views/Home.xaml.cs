using System;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.ErrorConsole;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public static Home Instance;
        public Home()
        {
            InitializeComponent();
            RootGrid.DataContext = MainWindow.User;
            Instance = this;
            MainWindow.Instance.CloudList.Width = 250;
            JoinDate.Text += MainWindow.User["user"]["member_since"].ToString().Split(' ')[0];
            MainWindow.Instance.showMenu.Visibility = Visibility.Visible;
            foreach (var cloud in MainWindow.Instance.CloudList.Items)
            {
                var cloudObject = (JObject) cloud;
                MessageSource.GetSource(cloudObject["id"].ToString()).UnreadMessages = 0;
            }
            Console.WriteLine(new User(MainWindow.User["user"]["id"].ToString()).Name);
        }

        private void ShowConsole(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var console = new ErrorConsole();
            console.Visibility = Visibility.Visible;
            console.Show();
        }
    }
}
