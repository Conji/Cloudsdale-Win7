using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CloudsdaleWin7.Properties;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.lib.Faye;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Helpers
{
    public class BrowserHelper
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

            JoinCloud(cloudObject);
        }

        private async static void JoinCloud(Cloud cloud)
        {
            var client = new HttpClient
                             {
                                 DefaultRequestHeaders =
                                     {
                                         {"Accept", "application/json"},
                                         {"X-Auth-Token", App.Connection.SessionController.CurrentSession.AuthToken}
                                     }
                             };
             

        }
    
    }
}
