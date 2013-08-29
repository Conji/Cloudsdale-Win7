using System;
using System.Net;
using System.Text;

namespace CloudsdaleWin7.lib.Models.Updaters
{
    //User Data Upload Model
    class UDUModel
    {
        private string Address = Endpoints.User.Replace("[:id]", (string) MainWindow.User["user"]["id"]);
        private const string _type = "application/json";
        private const string _method = "PUT";
        public static string Auth_Token = MainWindow.User["user"]["auth_token"].ToString();

        public static void Name(string name)
        {
            var dataObject = "{ \"user\" : { \"name\" : \"[:name]\"}}".Replace("[:name]", name);
            var data = Encoding.UTF8.GetBytes(dataObject);
            var request =
                WebRequest.CreateHttp(Endpoints.User.Replace("[:id]", MainWindow.User["user"]["id"].ToString()));
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = Auth_Token;
            
            request.BeginGetRequestStream(ar =>
            {
                var reqs = request.EndGetRequestStream(ar);
                reqs.Write(data, 0, data.Length);
                reqs.Close();
                request.BeginGetResponse(a =>
                {
                    try
                    {
                        var response = request.EndGetResponse(a);
                        response.Close();
                        MainWindow.User["user"]["name"] = name;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }
        public static void Username(string username)
        {
            var dataObject = "{ \"user\" : { \"username\" : \"[:username]\"}}".Replace("[:username]", username);
            var data = Encoding.UTF8.GetBytes(dataObject);
            var request =
                WebRequest.CreateHttp(Endpoints.User.Replace("[:id]", MainWindow.User["user"]["id"].ToString()));
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = Auth_Token;

            request.BeginGetRequestStream(ar =>
            {
                var reqs = request.EndGetRequestStream(ar);
                reqs.Write(data, 0, data.Length);
                reqs.Close();
                request.BeginGetResponse(a =>
                {
                    try
                    {
                        var response = request.EndGetResponse(a);
                        response.Close();
                        MainWindow.User["user"]["username"] = username;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }
        public static void Skype(string skype)
        {
            var dataObject = "{ \"user\" : { \"skype_name\" : \"[:skype]\"}}".Replace("[:skype]", skype);
            var data = Encoding.UTF8.GetBytes(dataObject);
            var request =
                WebRequest.CreateHttp(Endpoints.User.Replace("[:id]", MainWindow.User["user"]["id"].ToString()));
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = Auth_Token;

            request.BeginGetRequestStream(ar =>
            {
                var reqs = request.EndGetRequestStream(ar);
                reqs.Write(data, 0, data.Length);
                reqs.Close();
                request.BeginGetResponse(a =>
                {
                    try
                    {
                        var response = request.EndGetResponse(a);
                        response.Close();
                        MainWindow.User["user"]["skype_name"] = skype;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }
    }
}
