using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Helpers;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.Views.Flyouts
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        private readonly Session _current = App.Connection.SessionController.CurrentSession;
        private readonly Regex _nameRegex = new Regex("^[a-z_ ]+$", RegexOptions.IgnoreCase);
        private readonly Regex _usernameRegex = new Regex("^[a-z0-9]+$", RegexOptions.IgnoreCase);

        public Settings()
        {
            InitializeComponent();
            NameBlock.Text = _current.Name;
            UsernameBlock.Text = _current.Username;
            CheckChanges();
            SkypeBlock.Text = _current.SkypeName;
            AvatarImage.Source = new BitmapImage(_current.Avatar.Normal);
            if (App.Settings["notifications"] == "true") RcCheck.IsChecked = true;
        }
        private void CheckChanges()
        {
            if (_current.CanChangeName()) return;
            UsernameBlock.IsReadOnly = true;
        }

        private void ChangeName(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (!_nameRegex.IsMatch(NameBlock.Text))
            {
                NameBlock.Text = _current.Name;
                return;
            }
            App.Connection.SessionController.CurrentSession.UpdateProperty("name", NameBlock.Text);
        }

        private void Logout(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Connection.SessionController.Logout();
        }

        private void ReceiveTrue(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Connection.NotificationController.Receive = true;
            App.Settings.ChangeSetting("notifications", "true");
        }

        private void ReceiveFalse(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Connection.NotificationController.Receive = false;
            App.Settings.ChangeSetting("notifications", "false");
        }

        private void UploadNewAvatar(object sender, MouseButtonEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog
                             {
                                 DefaultExt = ".png",
                                 InitialDirectory = Environment.SpecialFolder.MyPictures.ToString(),
                                 Title = "Select a new avatar",
                                 Filter = "Image files | *.png; *.jpg; *.bmp"
                             };
            dialog.ShowDialog();

            var mimeType = "image/";

            if (String.IsNullOrEmpty(dialog.FileName)) return;
            if (dialog.SafeFileName == null) return;

            if (dialog.SafeFileName.EndsWith(".png")) mimeType += "png";
            else if (dialog.SafeFileName.EndsWith(".jpg")) mimeType += "jpg";
            else if (dialog.SafeFileName.EndsWith(".bmp")) mimeType += "bmp";
            else
            {
                dialog.Dispose();
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client, new Message{Content = "That's not a supported file type! Please try again."});
                return;
            }

            App.Connection.SessionController.CurrentSession.UploadAvatar(
                new FileStream(dialog.FileName, FileMode.Open), mimeType);
            dialog.Dispose();
            
        }

        private void ChangeSkypeName(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            App.Connection.SessionController.CurrentSession.UpdateProperty("skype_name", SkypeBlock.Text);
        }
    }
}
