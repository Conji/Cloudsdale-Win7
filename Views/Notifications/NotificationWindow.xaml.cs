using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using Message = CloudsdaleWin7.lib.Models.Message;

namespace CloudsdaleWin7.Views.Notifications
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow
    {
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

        public void Notify(NotificationType type, Message message)
        {
            
            switch (type)
            {
                case NotificationType.Client:
                    NoteTitle.Text = "";
                    NoteText.Text = message.Content;
                    break;
                case NotificationType.Cloud:
                    if (MainWindow.Instance.WindowState != WindowState.Minimized) return;
                    NoteTitle.Text = App.Connection.MessageController.CloudControllers[message.PostedOn].Cloud.Name;
                    NoteText.Text = message.FinalTimestamp + ".. @" + message.Author.Username + "-" + message.Content;
                    break;
            }
            ShowNote();
            HideNote();
        }
        
        public void ShowNote()
        {
            Visibility = Visibility.Visible;
            var a = new DoubleAnimation(0.0, 100.0, new Duration(new TimeSpan(0, 0, 3)))
                        {EasingFunction = new QuadraticEase()};
            Show();
            Opacity = 0.0;
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
