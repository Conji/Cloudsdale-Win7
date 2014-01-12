using System;
using System.Windows;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static MainWindow Instance;

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
            App.Connection.MainFrame = MainFrame;
            Testvoid();
            StateChanged += (sender, args) =>
                            {
                                if (WindowState == WindowState.Minimized)
                                    App.Connection.NotificationController.Receive = true;

                            };
            LostFocus += (sender, args) =>
                         {
                             App.Connection.NotificationController.Receive = true;
                         };
            GotFocus += (sender, args) =>
                        {
                            App.Connection.NotificationController.Receive = false;
                        };

            Closing += (sender, args) => App.Close();
            
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof (int), typeof (MainWindow),
                                        new FrameworkPropertyMetadata(200));

        private void Testvoid()
        {

        }
    }
}