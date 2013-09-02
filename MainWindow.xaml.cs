using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Models.Client;
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
            get { return (int) GetValue(MaxCharactersProperty); }
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
            MainFrame.Navigate(new Login());
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof (int), typeof (MainWindow),
                                        new FrameworkPropertyMetadata(200));

        private static readonly Dictionary<string, object> Clouds = new Dictionary<string, object>();

        private static object GetCloudView(JToken cloud)
        {
            if (Clouds.ContainsKey((string) cloud["id"]))
            {
                return Clouds[(string) cloud["id"]];
            }
            return Clouds[(string) cloud["id"]] = new CloudView(cloud);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsWindow.Instance.Close();
            }
            catch
            {
            }
            try
            {
                UserInfo.Instance.Close();
            }
            catch
            {
            }
        }

        private void SaveSettings(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings.Default.AppHeight = (int) Height;
            UserSettings.Default.AppWidth = (int) Width;
            UserSettings.Default.Save();
        }

    }
}