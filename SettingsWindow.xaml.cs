using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using Cloudsdale_Win7.Win7_Lib;
using Cloudsdale_Win7.Models;
using Cloudsdale_Win7.Cloudsdale;

namespace Cloudsdale_Win7
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public static SettingsWindow Instance;
        private static Regex NameRegex = new Regex(@"([A-Za-z0-9_]+)", RegexOptions.IgnoreCase);
        public bool UpdateClearable = false;

        public SettingsWindow()
        {
            InitializeComponent();
            Instance = this;

            if (User.NameChangesAllowed() == 0)
            {
                username.IsEnabled = false;
            }
            username.Text = MainWindow.User["user"]["username"].ToString();
            name.Text = MainWindow.User["user"]["name"].ToString();
            if (MainWindow.User["user"]["skype_name"] != null)
            {
                skype.Text = MainWindow.User["user"]["skype_name"].ToString();
            }
            aka.Text += MainWindow.User["user"]["also_known_as"].ToString().MultiReplace("[", "]", "\"", "");
        }
       
        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow.User = null;
            MainWindow.Instance.Frame.Navigate(new Login());
            Login.Instance.EmailBox.Text = UserSettings.Default.PreviousEmail;
            Login.Instance.PasswordBox.Password = UserSettings.Default.PreviousPassword;
            Login.Instance.autoSession.IsChecked = false;
            MainWindow.Instance.CloudList.ItemsSource = null;
            MainWindow.Instance.CloudList.Width = 5;
        }

        private void CheckLength(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password.Length >= 6)
            {
                var b = new SolidColorBrush();
                b.Color = Cloudsdale_Source.Success_Bright;
                newPassword.Background = b;
            }
            else
            {
                var b = new SolidColorBrush();
                b.Color = Cloudsdale_Source.Error_Bright;
                newPassword.Background = b;
            }
        }

        private void CheckContent(object sender, RoutedEventArgs e)
        {
            var input = username.Text;
            var b_success = new SolidColorBrush(Cloudsdale_Source.Success_Bright);
            var b_fail = new SolidColorBrush(Cloudsdale_Source.Error_Bright);
            foreach(var c in input.ToCharArray())
            {
                if(Char.IsLetterOrDigit(c) || c == '_')
                {
                    username.Background = b_success;
                    UpdateClearable = true;
                }else
                {
                    username.Background = b_fail;
                }
            }
        }

        private void Default(object sender, RoutedEventArgs e)
        {
            var b = new SolidColorBrush(Cloudsdale_Source.PrimaryBackground);
            username.Background = b;
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            UDUModel.Name(name.Text);
        }
    }
}
 