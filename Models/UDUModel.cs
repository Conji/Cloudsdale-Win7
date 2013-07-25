using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Cloudsdale_Win7.Assets;
using Cloudsdale_Win7.Cloudsdale;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7.Models
{
    //User Data Upload Model
    class UDUModel
    {
        public static string Address = Endpoints.User.Replace("[:id]", (string) MainWindow.User["user"]["id"]);
        public static string _type = "application/json";
        public static string _method = "POST";
        public static string Auth_Token = MainWindow.User["user"]["auth_token"].ToString();
        public static void Name(string name)
        {
            var dataObject = new JObject();
            dataObject["client_id"] = FayeConnector.ClientID;
            dataObject["user"]["name"] = name;
            var data = Encoding.UTF8.GetBytes(dataObject.ToString());
            var request =
                WebRequest.CreateHttp(Endpoints.User.Replace("[:id]", MainWindow.User["user"]["id"].ToString()));
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
        }
        public static void Username(string username)
        {
            
        }
        public static void Avatar(Image avatar)
        {
            
        }
        public static void Skype(string skype)
        {
            
        }
    }
}
