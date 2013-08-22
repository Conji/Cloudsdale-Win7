using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib
{
    class RestateMap
    {
        public static void RestateCloudList(JObject cloud)
        {
            if (cloud == null) return;
            var old = (string) MainWindow.User["user"]["clouds"];
            var newCloud = (string) cloud;
            Console.WriteLine(old);
        }
        public static void RestateCloudList(string cloudId)
        {
            if (cloudId == null) return;
            var req =
                WebRequest.CreateHttp(Endpoints.CloudJson.Replace("[:id]", cloudId));
            var r1 = req.GetResponse();
            var r2 = r1.GetResponseStream();
            var s1 = new StreamReader(r2).ReadToEnd();
            var j1 = JObject.Parse(s1);
            var result = j1["result"];

            bool foundMatch = false;
            foreach (var cloud in MainWindow.User["user"]["clouds"])
            {
                if (result["id"] != cloud["id"]) return;
                foundMatch = true;
                break;
            }
            if (foundMatch == false)
            {
                var oldList = MainWindow.User["user"]["clouds"].ToString();
                var newList = oldList.Insert(oldList.Length - 3, result.ToString());
                MainWindow.User["user"]["clouds"] = JObject.Parse(newList);
                MainWindow.Instance.CloudList.ItemsSource = MainWindow.User["user"]["clouds"];
            }
        }
    }
}
