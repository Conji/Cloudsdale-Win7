using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cloudsdale.connection;
using Cloudsdale.connection.MessageController;

namespace Cloudsdale.lib.Models
{
    public sealed class CloudModel
    {
        public string _ownerID;
        public string[] _moderatorIDs;
        public string[] _userIDs;
        public int _users;

        public string _rules;
        public string _description;
        public static string _id;
        public static string _shortname;
        public string _location;//will always start with Endpoints.BaseCloudAddress

        public int _dropcount;

        public string _avatar =
            "https://avatar-cloudsdale.netdna-ssl.com/cloud/[:id].png?s=24&mtime=1373579446".Replace("[:id]", _id);

        public string _name;

        public static JObject CloudJson(string cloudID)
        {
            WebRequest request = WebRequest.Create(Endpoints.CloudJson.Replace("[:id]", cloudID));
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            var responseReader = new StreamReader(responseStream);
            var responseData = JObject.Parse(responseReader.ReadToEnd());

            return (responseData);
        }
        public static string CloudName(string cloudID)
        {
            JObject cloud = CloudJson(cloudID);
            return cloud["result"]["name"].ToString();
        }
    }
}
