using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using CloudsdaleWin7.Controls;
using CloudsdaleWin7.Views.CloudViews;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

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
            CloudMessages.ItemsSource = App.Connection.MessageController[cloud].Messages;
            App.Connection.MessageController[cloud].LoadMessages();
            Main.Instance.HideFlyoutMenu();
            InputBox.Focus();
            App.Connection.MessageController[cloud].LoadBans();
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            SymbolBox.Visibility = SymbolBox.Visibility 
                == Visibility.Visible 
                ? Visibility.Hidden 
                : Visibility.Visible;
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
            
            Send(InputBox.Text);
        }
        internal async void Send(string message)
        {
            InputBox.IsEnabled = false;
            message = message.TrimEnd();

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
                await client.PostAsync(Endpoints.CloudMessages.Replace("[:id]", Cloud.Id), new JsonContent(messageData));
                InputBox.Text = "";
                await App.Connection.MessageController[Cloud].LoadMessages();
            }
            catch (Exception e) { Console.WriteLine(e.Message);}
            InputBox.Text = "";
            InputBox.IsEnabled = true;
            InputBox.Focus();
        }

        private async void ShowUserList(object sender, MouseButtonEventArgs e)
        {
            await App.Connection.MessageController.CurrentCloud.LoadUsers();
            Main.Instance.ShowFlyoutMenu(new UserList(App.Connection.MessageController.CurrentCloud));
        }

        private void AddEmoji(object sender, MouseButtonEventArgs e)
        {
            var block = (TextBlock) sender;
            InputBox.Text += block.Text;
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
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                          new Message {Content = ex.Message});
            }
            CloudMessages.IsEnabled = true;
        }

        private void ScreenShot(object sender, RoutedEventArgs e)
        {

        }
    }

    public class MessageTemplateSelector : DataTemplateSelector
    {
        protected DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var message = (Message)item;

            var actionTemplate = new DataTemplate(new ActionMessageView());
            var standardTemplate = new DataTemplate(new StandardMessageView());

            return Message.SlashMeFormat.IsMatch(message.Content) ? actionTemplate : standardTemplate;
        }
    }
}
