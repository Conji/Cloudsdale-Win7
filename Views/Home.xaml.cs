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
        private string Name = App.Connection.SessionController.CurrentSession.Name;
        public static Home Instance;
        public Home()
        {
            InitializeComponent();
            RootGrid.DataContext = MainWindow.User;
            Instance = this;
            WelcomeMessage(Name);
        }
        private void WelcomeMessage(string name)
        {
            //add random message generation
            var message = "Hi, [:name]!";
            Welcome.Text = message.Replace("[:name]", name);
        }
    }
}
