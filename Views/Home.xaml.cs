using System;
using System.Windows.Controls;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private string Name = App.Connection.SessionController.CurrentSession.Name;
        public static Home Instance;
        public Home()
        {
            InitializeComponent();
            Instance = this;
            Welcome.Text = WelcomeMessage(Name);
        }
        private static string WelcomeMessage(string name)
        {
            var r = new Random();
            String message;
            switch(r.Next(0,3))
            {
                case 0:
                    message = "Hi, [:name]!";
                    break;
                case 1:
                    message = "Welcome, [:name]!";
                    break;
                case 2:
                    message = "Welcome back, [:name].";
                    break;
                default:
                    message = "Hi there, [:name]!";
                    break;
            }
            return message.Replace("[:name]", name);
        }
    }
}
