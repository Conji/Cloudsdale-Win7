using System.Diagnostics;
using System.Net.Http;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.lib.Helpers
{
    public static class BrowserHelper
    {
        private static bool IsCloudLink(string link)
        {
            if (link.Contains("cloudsdale.org/clouds")) return true;
            return false;
        }

        public static void FollowLink(string uri)
        {
            if (!IsCloudLink(uri))
            {
                Process.Start(uri);
                return;
            }
            var client = new HttpClient().AcceptsJson();
            var cloudId = uri.Split('/')[4];
            var response = client.GetStringAsync(Endpoints.Cloud.Replace("[:id]", cloudId));
            var cloudObject = (JsonConvert.DeserializeObject<WebResponse<Cloud>>(response.Result)).Result;

            JoinCloud(cloudObject.Id);
        }

        private async static void JoinCloud(string id)
        {
            var client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    {"Accept", "application/json"},
                    {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                }
            };
            var response =  await client.PutAsync(Endpoints.CloudUserRestate.Replace("[:id]", id).ReplaceUserId(
                        App.Connection.SessionController.CurrentSession.Id), new StringContent(""));
            var responseText = await response.Content.ReadAsStringAsync();
            var fullMessage = await JsonConvert.DeserializeObjectAsync<WebResponse<Session>>(responseText);
            fullMessage.Result.CopyTo(App.Connection.SessionController.CurrentSession);
        }
        
        public async static void LeaveCloud(string id)
        {
            FayeConnector.Unsubscribe("/clouds/" + id + "/chat/messages");
            FayeConnector.Unsubscribe("clouds/" + id + "users/**");

            var client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    {"Accept", "application/json"},
                    {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                }
            };
            var response =
                await
                client.DeleteAsync(
                    Endpoints.CloudUserRestate.Replace("[:id]", id).ReplaceUserId(
                        App.Connection.SessionController.CurrentSession.AuthToken));
            var responseText = await response.Content.ReadAsStringAsync();
            var fullMessage = await JsonConvert.DeserializeObjectAsync<WebResponse<Session>>(responseText);
            fullMessage.Result.CopyTo(App.Connection.SessionController.CurrentSession);
        }
    }
}
