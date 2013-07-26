using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Cloudsdale_Win7.Assets;

namespace Cloudsdale_Win7.Models
{
    class CloudModel
    {
        public static JObject CloudJson(string cloudId)
        {
            WebRequest request = WebRequest.Create(Endpoints.CloudJson.Replace("[:id]", cloudId));
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            var responseReader = new StreamReader(responseStream);
            var responseData = JObject.Parse(responseReader.ReadToEnd());

            return responseData;
        }
        public static string Name(string cloudId)
        {
            JObject cloud = CloudJson(cloudId);
            return cloud["result"]["name"].ToString();
        }
        public static string Owner(string cloudId)
        {
            JObject cloud = CloudJson(cloudId);
            return cloud["result"]["owner_id"].ToString();
        }
        public static List<string> UserList(string cloudId)
        {
            JObject cloud = CloudJson(cloudId);
            var users = new List<string>();
            foreach (var user in cloud["result"]["user_ids"])
            {
                users.Add(user.ToString());
            }
            return users;
        }
        public static List<string> OnlineUsers(string cloudId)
        {
            JObject cloud = CloudJson(cloudId);
            var users = new List<string>();
            foreach (var user in cloud["result"]["id"])
            {
                users.Add(user.ToString());
            }
            return users;
        }
    }
}
