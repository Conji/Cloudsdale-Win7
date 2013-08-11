using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cloudsdale_Win7.Win7_Lib.Models
{
    class ClientVersion
    {
        public static string CLIENT_VERSION = "1.6";
        private static string _version;
        public static string UpdatedVersion()
        {
            var request = WebRequest.CreateHttp(Endpoints.VersionAddress);
            var responseStream = request.GetResponse().GetResponseStream();
            var response = new StreamReader(responseStream);
            return response.ReadToEnd();
        }
    }
}
