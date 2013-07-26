using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Cloudsdale_Win7.Models;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        public static MainWindow Instance;
        public static JObject User;
        public static JToken CurrentCloud;
        public int MaxCharacters {
            get { return (int)GetValue(MaxCharactersProperty); }
            set { SetValue(MaxCharactersProperty, value); }
        }

        public MainWindow() {
            InitializeComponent();
            Instance = this;
        }

        private void CloudListSelectionChanged(object sender, SelectionChangedEventArgs e) {
            CurrentCloud = (JToken)CloudList.SelectedItem;
            Frame.Navigate(GetCloudView(CurrentCloud));
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof(int), typeof(MainWindow),
                                        new FrameworkPropertyMetadata(200));
        private static readonly Dictionary<string, object> Clouds = new Dictionary<string, object>();
        private static object GetCloudView(JToken cloud) {
            if (Clouds.ContainsKey((string)cloud["id"])) {
                return Clouds[(string)cloud["id"]];
            }
            return Clouds[(string)cloud["id"]] = new CloudView(cloud);
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
