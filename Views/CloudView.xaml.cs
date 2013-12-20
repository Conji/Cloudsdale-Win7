using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CloudsdaleWin7.Views.CloudViews;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;
using CommandManager = CloudsdaleWin7.lib.CloudsdaleLib.Misc.Commands.CommandManager;

namespace CloudsdaleWin7.Views {
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView
    {
        public static CloudView Instance;
        public Cloud Cloud { get; set; }
        
        public CloudView(Cloud cloud)
        {
            InitializeComponent();
            Instance = this;
            Cloud = cloud;
            App.Connection.MessageController[cloud].UnreadMessages = 0;
            Name.Text = cloud.Name;
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
            CloudMessages.ItemsSource = MessageSource.GetSource(cloud.Id).Messages;
            Main.Instance.Frame.IsEnabled = true;
            Main.Instance.LoadingText.Visibility = Visibility.Hidden;
            Main.Instance.HideFlyoutMenu();
            InputBox.Focus();
        }

        private void SendBoxEnter(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputBox.Text)) return;
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
            if (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.Enter)
            {
                ((TextBox)sender).Text += "\r\n";
                ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
                return;
            }
            if (e.Key != Key.Enter) return;

            if (InputBox.Text.StartsWith("/") && !InputBox.Text.StartsWith("//") && !InputBox.Text.StartsWith("/me"))
            {
                if (!CommandManager.TryExecuteCommand(InputBox.Text)) return;
                InputBox.Text = "";
                return;
            }
            
            Send(InputBox.Text);
        }
        internal async void Send(string message)
        {
            message = message.TrimEnd().Replace("\r", "");

            var messageModel = new Message
            {
                Content = message.EscapeLiteral(),
                Device = "desktop",
                ClientId = FayeConnector.ClientId
            };

            var messageData = await JsonConvert.SerializeObjectAsync(messageModel);

            messageModel.Id = Guid.NewGuid().ToString();
            messageModel.Author = App.Connection.SessionController.CurrentSession;

            var client = new HttpClient
            {
                DefaultRequestHeaders = {
                    { "Accept", "application/json" }, 
                    { "X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken }
                }
            };

            try
            {
                InputBox.IsEnabled = false;
                await client.PostAsync(Endpoints.CloudMessages.Replace("[:id]", Cloud.Id), new JsonContent(messageData));
                InputBox.Text = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                InputBox.IsEnabled = true;}
            InputBox.Text = "";
            InputBox.Focus();
        }

        private async void ShowUserList(object sender, MouseButtonEventArgs e)
        {
            await App.Connection.MessageController.CurrentCloud.LoadUsers();
            Main.Instance.ShowFlyoutMenu(new UserList(App.Connection.MessageController.CurrentCloud));
        }

        private void ExpandCloud(object sender, RoutedEventArgs e)
        {
            Page prefFlyout;
            if (Cloud.OwnerId == App.Connection.SessionController.CurrentSession.Id)
            {
                prefFlyout = new OwnedCloud(Cloud);
            }else
            {
                prefFlyout = new StandardCloud(Cloud);
            }
            Main.Instance.ShowFlyoutMenu(prefFlyout);
        }

        private async void Reload(object sender, RoutedEventArgs e)
        {
            CloudMessages.IsEnabled = false;
            try
            {
                await App.Connection.MessageController[Cloud].LoadMessages();
                await Cloud.ForceValidate();
            }
            catch (Exception ex)
            {
                App.Connection.NotificationController.Notification.Notify(ex.Message);
            }
            CloudMessages.IsEnabled = true;
        }

        private void MinimizeFlyout(object sender, MouseButtonEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }
    }
}
