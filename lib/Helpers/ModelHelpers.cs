using System;
using System.Net.Http;
using System.Threading.Tasks;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Notifications;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Helpers
{
    public static class ModelHelpers
    {
        public static async Task UpdateProperty(this Cloud cloud, string property, JToken value, bool stayOnCloud = false)
        {
            var currentIndex = Main.Instance.Clouds.SelectedIndex;

            var o = @"{""cloud"":{""[:p]"":""[:v]""}}"
               .Replace("[:p]", property)
               .Replace("[:v]", value.ToString());
            Console.WriteLine(o);

            var client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                }
            }.AcceptsJson();
            var response = await client.PostAsync(Endpoints.Cloud.Replace("[:id]", cloud.Id), new JsonContent(o));
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            var responseObject = JsonConvert.DeserializeObject<WebResponse<Cloud>>(await response.Content.ReadAsStringAsync());

            if (responseObject.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(responseObject.Flash.Message);
                return;
            }
            responseObject.Result.CopyTo(App.Connection.MessageController[cloud].Cloud);
            App.Connection.SessionController.RefreshClouds();
            Main.Instance.InitSession();

            if (stayOnCloud != true) return;
            Main.Instance.Clouds.SelectedIndex = currentIndex;
        }

        public static async Task UpdateProperty(this User user, string property, JToken value)
        {
            var o = @"{""user"":{""[:p]"":""[:v]""}}"
                .Replace("[:p]", property)
                .Replace("[:v]", value.ToString());

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
                client.PutAsync(Endpoints.User.Replace("[:id]", App.Connection.SessionController.CurrentSession.Id),
                                 new JsonContent(o));
            var responseObject =
                JsonConvert.DeserializeObjectAsync<WebResponse<User>>(await response.Content.ReadAsStringAsync());
            if (responseObject.Result.Flash != null)
            {
                App.Connection.NotificationController.Notification.Notify(responseObject.Result.Flash.Message);
                return;
            }
            responseObject.Result.Result.CopyTo(App.Connection.SessionController.CurrentSession);
            responseObject.Result.Result.CopyTo(App.Connection.ModelController.Users[App.Connection.SessionController.CurrentSession.Id]);
            Main.Instance.InitSession();
        }
    }
}
