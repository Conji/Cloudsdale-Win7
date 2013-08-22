using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.MVVM;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static MainWindow Instance;
        public static JObject User;
        public static JToken CurrentCloud;
        public static int CloudIndex;
        public static string WebMessage;
        
        public int MaxCharacters
        {
            get { return (int)GetValue(MaxCharactersProperty); }
            set { SetValue(MaxCharactersProperty, value); }
        }

        public MainWindow()
        {
            Instance = this;
            ClientVersion.CleanUp();
            ClientVersion.CheckVersion();
            InitializeComponent();
            Height = UserSettings.Default.AppHeight;
            Width = UserSettings.Default.AppWidth;

        }

        private void CloudListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CloudList.SelectedIndex >= 0)
            {
                CurrentCloud = (JToken)CloudList.SelectedItem;
                Frame.Navigate(GetCloudView(CurrentCloud));
                if (User["user"]["id"].ToString() != CurrentCloud["owner_id"].ToString())
                {
                    CloudOwnerItem.Visibility = Visibility.Collapsed;
                }else{ CloudOwnerItem.Visibility = Visibility.Visible; }
                CloudIndex = CloudList.SelectedIndex;
                
            }
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof(int), typeof(MainWindow),
                                        new FrameworkPropertyMetadata(200));
        private static readonly Dictionary<string, object> Clouds = new Dictionary<string, object>();
        private static object GetCloudView(JToken cloud)
        {
            if (Clouds.ContainsKey((string)cloud["id"]))
            {
                return Clouds[(string)cloud["id"]];
            }
            return Clouds[(string)cloud["id"]] = new CloudView(cloud);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsWindow.Instance.Close();
            }catch
            {
            }
            try
            {
                UserInfo.Instance.Close();
            }catch
            {
            }
        }

        private void SaveSettings(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings.Default.AppHeight = (int)Height;
            UserSettings.Default.AppWidth = (int)Width;
            UserSettings.Default.Save();
        }

        private void KeepCurrentCloud(object sender, MouseButtonEventArgs e)
        {
            CloudList.SelectedIndex = CloudIndex;
        }

        private void GetCloudId(object sender, MouseButtonEventArgs e)
        {
            var item = (ListViewItem) sender;
            Console.WriteLine(item.DataContext.ToString());
        }

        private void ShowMenu(object sender, MouseButtonEventArgs e)
        {
            if (MenuPanel.IsVisible)
            {
                showMenu.Text = "";
                MenuPanel.Visibility = Visibility.Hidden;
            }else
            {
                showMenu.Text = "";
                MenuPanel.Visibility = Visibility.Visible;
            }
        }

        private void DirectHome(object sender, MouseButtonEventArgs e)
        {
            Frame.Navigate(new Home());
        }

        private void ShowSettings(object sender, MouseButtonEventArgs e)
        {
            var settings = new SettingsWindow();
            settings.Show();
        }

        private void DirectToBrowser(object sender, MouseButtonEventArgs e)
        {
            Frame.Navigate(new Browser());
        }
        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            User = null;
            Login.Logout();
            Instance.Frame.Navigate(new Login());
            Instance.CloudList.ItemsSource = null;
            Instance.CloudList.Width = 4;
        }

        private void ExpandMenu_Completed(object sender, EventArgs e)
        {

        }

        private void ShrinkMenu_Completed(object sender, EventArgs e)
        {

        }
    }
}