using System.Windows;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Initial
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm
    {
        public Confirm()
        {
            InitializeComponent();
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            Waiting.Visibility = Visibility.Visible;
            if (App.Connection.SessionController.CurrentSession.NeedsToConfirmRegistration == false)
            {
                Waiting.Visibility = Visibility.Hidden;
            }
            else
            {
                if (App.Connection.SessionController.CurrentSession.HasReadTnc == false)
                {
                    App.Connection.MainFrame.Navigate(new TermsAndConditions());
                }
                else
                {
                    App.Connection.MainFrame.Navigate(new Main());
                }
            }
        }
    }
}
