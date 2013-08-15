using System;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Models.Updaters;

namespace CloudsdaleWin7
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

        private void CheckLength(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password.Length >= 6)
            {
                var b = new SolidColorBrush();
                b.Color = CloudsdaleSource.SuccessBright;
                newPassword.Background = b;
            }
            else
            {
                var b = new SolidColorBrush();
                b.Color = CloudsdaleSource.ErrorBright;
                newPassword.Background = b;
            }
        }

        private void CheckContent(object sender, RoutedEventArgs e)
        {
            var input = username.Text;
            var b_success = new SolidColorBrush(CloudsdaleSource.SuccessBright);
            var b_fail = new SolidColorBrush(CloudsdaleSource.ErrorBright);
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
            var b = new SolidColorBrush(CloudsdaleSource.PrimaryBackground);
            username.Background = b;
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            UDUModel.Name(name.Text);
        }

        private void CheckInput(object sender, TextChangedEventArgs e)
        {
            foreach (char letter in name.Text.ToCharArray())
            {
                if (!Char.IsLetter(letter))
                {
                    name.Text.Replace(letter, ' ');
                }
            }
        }
    }
}
 