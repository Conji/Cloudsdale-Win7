using System;
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
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView {
        public static CloudView Instance;
        private static Cloud _cloud;
        private static CloudController CloudInstance = new CloudController(_cloud);

        public CloudView(Cloud cloud)
        {
            _cloud = cloud;
            Instance = this;
            InitializeComponent();
            Name.Text = cloud.Name;

            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SymbolBox.Visibility == Visibility.Visible)
            {
                SymbolBox.Visibility = Visibility.Hidden;
            }else
            {
                SymbolBox.Visibility = Visibility.Visible;
            }
        }
        private void ShowCloudInfo(object sender, MouseButtonEventArgs args)
        {
            
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
            dataObject["client_id"] = App.Connection.Faye.ClientId;
            dataObject["device"] = "desktop";
            var data = Encoding.UTF8.GetBytes(dataObject.ToString());
            var request = WebRequest.CreateHttp(Endpoints.CloudMessages.Replace("[:id]", _cloud.Id));
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }

        private void ShowUserList(object sender, MouseButtonEventArgs e)
        {
            Main.Instance.ShowFlyoutMenu(new UserList(_cloud));
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
