using System;
using System.Windows;
using CloudsdaleWin7.Views.LoadingViews;
using CloudsdaleWin7.lib.CloudsdaleLib;

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

        public static void Logout()
        {
            LoggingOut = true;
            Instance.EmailBox.Text = UserSettings.Default.PreviousEmail;
            Instance.PasswordBox.Password = UserSettings.Default.PreviousPassword;
            Instance.autoLogin.IsChecked = false;
        }

        private async void LoginClick(object sender, RoutedEventArgs e)
        {

            try {
                await App.Connection.SessionController.Login(EmailBox.Text, PasswordBox.Password);
                UserSettings.Default.PreviousEmail = EmailBox.Text;
                UserSettings.Default.PreviousPassword = PasswordBox.Password;
                UserSettings.Default.AutoLogin = autoLogin.IsChecked.Value;
                UserSettings.Default.Save();
                MainWindow.Instance.MainFrame.Navigate(new LoadLogin());
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ClearText(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (EmailBox.Text.Trim().Contains(" "))
            {
                EmailBox.Text = "";
                PasswordBox.Password = "";
            }
        }
    }
}
