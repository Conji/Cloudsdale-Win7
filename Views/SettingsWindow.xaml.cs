using System;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cloudsdale_Win7.Win7_Lib;
using Cloudsdale_Win7.Win7_Lib.Cloudsdale_Lib;
using Cloudsdale_Win7.Win7_Lib.Models;
using Cloudsdale_Win7.Win7_Lib.Models.Updaters;

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

            if (MainWindow.User["user"]["username_changes_allowed"].ToString() == "0")
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

            avatar.Source = new BitmapImage(new Uri(MainWindow.User["user"]["avatar"]["preview"].ToString(), UriKind.Absolute));
            avatar.Stretch = Stretch.UniformToFill;
        }
       
        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow.User = null;
            Login.Logout();
            MainWindow.Instance.Frame.Navigate(new Login());
            MainWindow.Instance.CloudList.ItemsSource = null;
            MainWindow.Instance.CloudList.Width = 4;
            Close();
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
 