using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        public void CloseSettings(object sender, RoutedEventArgs e)
        {
            Instance.Visibility = Visibility.Collapsed;
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
