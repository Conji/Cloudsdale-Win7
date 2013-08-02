using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cloudsdale_Win7.Assets;
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

        public SettingsWindow()
        {
            InitializeComponent();
            Instance = this;

            if (UserModel.NameChangesAllowed() == 0)
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
        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (name.IsFocused)
            {
                var input = name.Text;
                var hasSymbol = !String.IsNullOrEmpty(input) && !Char.IsLetterOrDigit(input[input.Length - 1]);

                if (hasSymbol)
                {
                    var b = new SolidColorBrush();
                    b.Color = Cloudsdale_Source.Error_Bright;
                    name.Background = b;
                }
                else
                {
                    var b = new SolidColorBrush();
                    b.Color = Cloudsdale_Source.Success_Bright;
                    name.Background = b;
                }
            }
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Frame.Navigate(new Login());
            Login.Instance.EmailBox.Text = UserSettings.Default.PreviousEmail;
            Login.Instance.PasswordBox.Password = UserSettings.Default.PreviousPassword;
            Login.Instance.autoSession.IsChecked = false;
            MainWindow.Instance.CloudList.ItemsSource = null;
            MainWindow.User = null;
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
    }
}
 