using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.Views.Initial;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public static Login Instance;
        public static bool LoggingOut = false;

        public Login()
        {
            Instance = this;
            InitializeComponent();
            EmailBox.Text = App.Settings["email"];
            if (String.IsNullOrEmpty(EmailBox.Text)) EmailBox.Focus();
            if (!String.IsNullOrEmpty(EmailBox.Text) && String.IsNullOrEmpty(PasswordBox.Password)) PasswordBox.Focus();
            VersionBlock.Text = ClientVersion.Version;
            Title = "Cloudsdale";
        }

        private async void LoginAttempt(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (String.IsNullOrEmpty(EmailBox.Text) || String.IsNullOrEmpty(PasswordBox.Password))
            {
                ShowMessage("You can't have empty fields, silly filly. Try again.");
                return;
            }
            ErrorMessage.Text = "";
            LoginUi.SwitchVisibility(LoggingInUi);
            try
            {
                if (App.Settings["email"] != EmailBox.Text) App.Settings.Clear();
                App.Settings["email"] = EmailBox.Text;
                await App.Connection.SessionController.Login(EmailBox.Text, PasswordBox.Password);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                LoginUi.SwitchVisibility(LoggingInUi);
            }
        }

        public void ShowMessage(string message)
        {
            #region Show Message

            ErrorMessage.Text = message;
            ErrorMessage.BeginAnimation(OpacityProperty, new DoubleAnimation(0.0, 100.0, new Duration(new TimeSpan(200000000))));

            #endregion
        }

        private void LaunchReg(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.MainFrame.Navigate(new Register());
        }
    }
}
