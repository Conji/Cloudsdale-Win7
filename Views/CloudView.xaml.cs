using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CloudsdaleWin7.Controls;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7 {
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
            App.Connection.MessageController[cloud].EnsureLoaded();
            App.Connection.MessageController[cloud].UnreadMessages = 0;
            Name.Text = cloud.Name;
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
            CloudMessages.ItemsSource = App.Connection.MessageController[cloud].Messages;
            Main.Instance.FlyoutFrame.Navigate(new UserList(App.Connection.MessageController[cloud]));
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            if (SymbolBox.Visibility == Visibility.Visible)
            {
                SymbolBox.Visibility = Visibility.Hidden;
                return;
            } SymbolBox.Visibility = Visibility.Visible;
        }

        private void SendBoxEnter(object sender, KeyEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));

            if (e.Key != Key.Enter) return;
            if (string.IsNullOrWhiteSpace(InputBox.Text)) return;
            Send(InputBox.Text);
        }
        internal async void Send(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            InputBox.IsEnabled = false;
            message = message.TrimEnd();


            var messageModel = new Message
            {
                Content = message.EscapeLiteral(),
                Device = "desktop",
                ClientId = FayeConnector.ClientID
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
                
                var response = await client.PostAsync(Endpoints.CloudMessages.Replace("[:id]", Cloud.Id),
                new StringContent(messageData)
                {
                    Headers =
                        {
                        ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                );
                var responseText = await response.Content.ReadAsStringAsync();
                var fullMessage = await JsonConvert.DeserializeObjectAsync<WebResponse<Message>>(responseText);

                if (fullMessage == null) return;
                if (fullMessage.Flash != null)
                {
                    await App.Connection.ErrorController.OnError(fullMessage);
                    return;
                }

                fullMessage.Result.PreProcess();
                fullMessage.Result.CopyTo(messageModel);
                App.Connection.MessageController[Cloud].AddMessageToSource(messageModel);
            }
            catch (Exception e) { Console.WriteLine(e.Message);}

            InputBox.Text = "";
            InputBox.IsEnabled = true;
            InputBox.Focus();
        }

        private async void ShowUserList(object sender, MouseButtonEventArgs e)
        {
            Main.Instance.ShowFlyoutMenu(new UserList(App.Connection.MessageController.CurrentCloud));
            await App.Connection.MessageController.CurrentCloud.LoadUsers();
        }

        private void AddEmoji(object sender, MouseButtonEventArgs e)
        {
            var block = (TextBlock) sender;
            InputBox.Text += block.Text;
        }
    }
    public class MessageTemplateSelector : DataTemplateSelector
    {
        protected DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var message = (Message)item;

            var actionTemplate = new DataTemplate(new ActionMessageView());
            var standardTemplate = new DataTemplate(new StandardMessageView());

            if (Message.SlashMeFormat.IsMatch(message.Content))
            {
                return actionTemplate;
            }
            return standardTemplate;
        }
    }
}
