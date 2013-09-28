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

        private static void JoinCloud(Cloud cloud)
        {
            var jObj = new JObject();
            var dataString = jObj.ToString();
            var data = Encoding.UTF8.GetBytes(dataString);
            var request = WebRequest.CreateHttp(Endpoints.CloudUserRestate.
                Replace("[:id]", cloud.Id).
                ReplaceUserId(App.Connection.SessionController.CurrentSession.Id));
            request.Accept = "application/json";
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Headers["X-Auth-Token"] = App.Connection.SessionController.CurrentSession.AuthToken;
            request.BeginGetRequestStream(ar =>
            {
                var requestStream = request.EndGetRequestStream(ar);
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                request.BeginGetResponse(a =>
                {
                    try
                    {
                        var response = request.EndGetResponse(a);
                        string message;
                        using (var responseStream = response.GetResponseStream())
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            message = streamReader.ReadToEnd();
                        }
                        var user = JsonConvert.DeserializeObject<WebResponse<Session>>(message).Result;
                        user.CopyTo(App.Connection.SessionController.CurrentSession);
                    }
                    catch (WebException ex){ }
                }, null);
            }, null);

        }
    
    }
}
