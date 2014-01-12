using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CloudsdaleWin7.lib.Helpers;
using Message = CloudsdaleWin7.lib.Models.Message;

namespace CloudsdaleWin7.Views.Notifications
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow
    {

        private Message Message { get; set; }

        public NotificationWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
        }

        void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 20;
            Top = 20;
        }

        /// <summary>
        /// Creates a simple notification with the title "Cloudsdale".
        /// </summary>
        /// <param name="message"></param>
        public void Notify(string message)
        {
            Notify(NotificationType.Client, new Message {Content = message});
        }

        public void Notify(NotificationType type, Message message)
        {
            Message = message;
            switch (type)
            {
                case NotificationType.Client:
                    NoteTitle.Text = "Cloudsdale";
                    NoteText.Text = message.Content;
                    NoteTitle.MouseDown += ShowMain;
                    break;
                case NotificationType.Cloud:
                    if (MainWindow.Instance.WindowState != WindowState.Minimized) return;
                    NoteTitle.Text = App.Connection.MessageController.CloudControllers[message.PostedOn].Cloud.Name;
                    NoteText.Text = message.FinalTimestamp + ".. @" + message.Author.Username + ": \r\n" + message.Content.TrimToLength(100).Replace(@"\n", @"\r\n");
                    NoteTitle.MouseDown += DirectToCloud;
                    break;
            }
            ShowNote();
            HideNote();
        }
        
        public void ShowNote()
        {
            Visibility = Visibility.Visible;
            Show();
            var a = new DoubleAnimation(0.0, 100.0, new Duration(new TimeSpan(0, 0, 3)))
                        {EasingFunction = new QuadraticEase()};
            BeginAnimation(OpacityProperty, a);
            
        }

        public void HideNote()
        {
            var a = new DoubleAnimation(100.0, 0.0, new Duration(new TimeSpan(0, 0, 6)))
                        {EasingFunction = new ExponentialEase()};
            BeginAnimation(OpacityProperty, a);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        private void DirectToCloud(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.WindowState = WindowState.Normal;
            Main.Instance.Clouds.SelectedItem =
                Main.Instance.Clouds.Items.IndexOf(App.Connection.ModelController.Clouds[Message.PostedOn]);
            WindowState = WindowState.Minimized;
        }

        private void ShowMain(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.WindowState = WindowState.Normal;
            WindowState = WindowState.Minimized;
        }

    }

    /// <summary>
    /// Cloud, user, and client.
    /// To pass a client message, just create a new message and set the Content.
    /// </summary>
    public enum NotificationType
    {
        Cloud, Client
    }
}
