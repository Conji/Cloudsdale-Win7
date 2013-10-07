using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Notifications
{
    /// <summary>
    /// Interaction logic for CloudNote.xaml
    /// </summary>
    public partial class CloudNote : Page
    {

        private Message Message { get; set; }

        public CloudNote(Message message)
        {
            InitializeComponent();
            Message = message;
            NoteTitle.Text = App.Connection.MessageController.CloudControllers[message.PostedOn].Cloud.Name + "-";
            NoteContent.Text = "[" + message.NoteTimestamp + "] @" + message.Author.Username + ": " + message.Content;
        }

        private void DirectToCloud(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow.Instance.WindowState = WindowState.Maximized;
            MainWindow.Instance.Activate();
            Main.Instance.Clouds.SelectedValue = App.Connection.MessageController.CloudControllers[Message.PostedOn];
        }
    }
}
