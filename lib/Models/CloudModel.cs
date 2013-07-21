using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using Cloudsdale.lib.connection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cloudsdale.lib;
using Cloudsdale.lib.MessageController;

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

        public string _avatar;

        public string _name;

        public static JObject CloudJson(string cloudID)
        {
            WebRequest request = WebRequest.Create(Endpoints.CloudJson.Replace("[:id]", cloudID));
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            var responseReader = new StreamReader(responseStream);
            var responseData = JObject.Parse(responseReader.ReadToEnd());

            return responseData;
        }
        public static string CloudName(string cloudID)
        {
            JObject cloud = CloudJson(cloudID);
            return cloud["result"]["name"].ToString();
        }
        public static string OwnerID(string cloudID)
        {
            JObject cloud = CloudJson(cloudID);
            return cloud["result"]["owner_id"].ToString();
        }
        public static List<string> ModeratorIDs(string cloudID)
        {
            JObject cloud = CloudJson(cloudID);
            var ModIDs = new List<string>();
            foreach (var mod in CloudJson(cloudID)["result"]["moderator_ids"])
            {
                ModIDs.Add(mod.ToString());
            }
            return ModIDs;
        }
    }
}
