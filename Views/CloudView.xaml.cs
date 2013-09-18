using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.Cloud;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView
    {
        public static CloudView Instance;
        private static CloudController CloudInstance { get; set; }

        public CloudView(Cloud cloud)
        {
            InitializeComponent();
            Instance = this;
            CloudInstance = App.Connection.MessageController[cloud];
            CloudInstance.UnreadMessages = 0;
            Name.Text = CloudInstance.Cloud.Name;
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
            CloudMessages.ItemsSource = CloudInstance.Messages;
            Main.Instance.FlyoutFrame.Navigate(new UserList(CloudInstance));
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
        internal void Send(string message)
        {
            var dataObject = new JObject();
            dataObject["content"] = message.EscapeMessage();
            dataObject["client_id"] = FayeConnector.ClientID;
            dataObject["device"] = "desktop";
            var data = Encoding.UTF8.GetBytes(dataObject.ToString());
            var request = WebRequest.CreateHttp(Endpoints.CloudMessages.Replace("[:id]", App.Connection.MessageController.CurrentCloud.Cloud.Id));
            request.Accept = "application/json";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = App.Connection.SessionController.CurrentSession.AuthToken;
            request.BeginGetRequestStream(ar =>
            {
                var reqs = request.EndGetRequestStream(ar);
                reqs.Write(data, 0, data.Length);
                reqs.Close();
                request.BeginGetResponse(a =>
                {
                    try
                    {
                        var response = request.EndGetResponse(a);
                        response.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
            InputBox.Text = "";
        }

        private async void ShowUserList(object sender, MouseButtonEventArgs e)
        {
            Main.Instance.ShowFlyoutMenu(new UserList(CloudInstance));
            await CloudInstance.LoadUsers();
        }
    }
    public class MessageTemplateSelector : DataTemplateSelector
    {
        protected DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var message = (Message)item;
            var element = (FrameworkElement)container;
            if (Message.SlashMeFormat.IsMatch(message.Content))
            {
                return (DataTemplate)element.Resources["ActionChatTemplate"];
            }
            return (DataTemplate)element.Resources["StandardChatTemplate"];
        }
    }
}
