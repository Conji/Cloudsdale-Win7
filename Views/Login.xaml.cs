using System;
using System.Windows;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.LoadingViews;
using CloudsdaleWin7.lib.CloudsdaleLib;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login Instance;
        public static bool LoggingOut = false;

        public Login()
        {
           
            Instance = this;
            InitializeComponent();
            EmailBox.Text = UserSettings.Default.PreviousEmail;
            PasswordBox.Password = UserSettings.Default.PreviousPassword;
            autoLogin.IsChecked = UserSettings.Default.AutoLogin;
            if (LoggingOut == false)
            {
                if (autoLogin.IsChecked == true)
                {
                    LoginClick(LoginButton, null);
                }
            }
        }

        public void Logout()
        {
            LoggingOut = true;
            EmailBox.Text = UserSettings.Default.PreviousEmail;
            PasswordBox.Password = UserSettings.Default.PreviousPassword;
            autoLogin.IsChecked = false;
        }

        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            await App.Connection.SessionController.Login(EmailBox.Text, PasswordBox.Password);
            MainWindow.Instance.MainFrame.Navigate(new LoadLogin());
            UserSettings.Default.PreviousEmail = EmailBox.Text;
            UserSettings.Default.PreviousPassword = PasswordBox.Password;
            UserSettings.Default.AutoLogin = autoLogin.IsChecked.Value;
            UserSettings.Default.Save();
            if (App.Connection.SessionController.CurrentSession != null)
            {
                MainWindow.Instance.MainFrame.Navigate(new Main());
            }
            else
            {
                MainWindow.Instance.MainFrame.Navigate(new Login());
                ErrorMessage.Text = "Could not log in! Be sure to check if your credentials are correct.";
            }
        }

        private void ClearText(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (EmailBox.Text == "Email")
            {
                EmailBox.Text = "";
                PasswordBox.Password = "";
            }
        }
    }
}
