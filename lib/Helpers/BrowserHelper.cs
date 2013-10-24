using System;
using System.Diagnostics;
using System.Net.Http;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.lib.Helpers
{
    public static class BrowserHelper
    {
        private static bool IsCloudLink(string link)
        {
            return link.StartsWith("http://www.cloudsdale.org/clouds/") 
                | link.StartsWith("www.cloudsdale.org/clouds/") 
                | link.StartsWith("https://www.cloudsdale.org/clouds");
        }

        public static void FollowLink(string uri)
        {
            if (!IsCloudLink(uri))
            {
                try
                {
                    Process.Start(uri);
                }
                catch(Exception ex)
                {
                    App.Connection.NotificationController.Notification.Notify(NotificationType.Client,
                                                                              new Message {Content = ex.Message});
                }
                return;
            }
            try
            {
                var client = new HttpClient().AcceptsJson();
                var cloudId = uri.Split('/')[4];
                var response = client.GetStringAsync(Endpoints.Cloud.Replace("[:id]", cloudId));
                var cloudObject = (JsonConvert.DeserializeObject<WebResponse<Cloud>>(response.Result)).Result;

                JoinCloud(cloudObject);
            }catch(Exception ex)
            {
                App.Connection.NotificationController.Notification.Notify(NotificationType.Client, new Message{Content = ex.Message});
            }
        }

        public async static void JoinCloud(Cloud cloud)
        {
            if (App.Connection.MessageController.CloudControllers.ContainsKey(cloud.Id))
            {
                Main.Instance.Clouds.SelectedItem = cloud;
                return;
            }

            var client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    {"Accept", "application/json"},
                    {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                }
            };
            await client.PutAsync(Endpoints.CloudUserRestate.Replace("[:id]", cloud.Id).ReplaceUserId(
                        App.Connection.SessionController.CurrentSession.Id), new StringContent(""));

            App.Connection.SessionController.CurrentSession.Clouds.Add(cloud);
            App.Connection.MessageController.CloudControllers.Add(cloud.Id, new CloudController(cloud));
            App.Connection.SessionController.RefreshClouds();
            Main.Instance.Clouds.SelectedIndex = Main.Instance.Clouds.Items.Count - 1;
            FayeConnector.Subscribe("/clouds/" + cloud.Id + "/chat/messages");
        }
    }
}
