using System.Windows;
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
            get { return (int) GetValue(MaxCharactersProperty); }
            set { SetValue(MaxCharactersProperty, value); }
        }

        public MainWindow()
        {
            Instance = this;
            ClientVersion.Validate();
            InitializeComponent();
            MainFrame.Navigate(new Login());
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof (int), typeof (MainWindow),
                                        new FrameworkPropertyMetadata(200));

        private void SaveSettings(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings.Default.AppHeight = (int) Height;
            UserSettings.Default.AppWidth = (int) Width;
            UserSettings.Default.Save();
        }

    }
}