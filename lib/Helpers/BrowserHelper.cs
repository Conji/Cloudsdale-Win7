using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            var cloudId = uri.StartsWith("http:") ? uri.Split('/')[4] : uri.Split('/')[2];
            var response = client.GetStringAsync(Endpoints.Cloud.Replace("[:id]", cloudId));
            
        }
    }
}
