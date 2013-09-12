using System;
using System.Net;
using System.Text;

namespace CloudsdaleWin7.lib.Models.Updaters
{
    //User Data Upload Model
    class UDUModel
    {
        private static Session Current = App.Connection.SessionController.CurrentSession;
        private static string Address = Endpoints.User.Replace("[:id]", Current.Id);
        private const string _type = "application/json";
        private const string _method = "PUT";
        public static string AuthToken = Current.AuthToken;

        public static void Name(string name)
        {
            var dataObject = "{ \"user\" : { \"name\" : \"[:name]\"}}".Replace("[:name]", name);
            var data = Encoding.UTF8.GetBytes(dataObject);
            var request =
                WebRequest.CreateHttp(Address);
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = AuthToken;
            
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
                        Current.Name = name;
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
                WebRequest.CreateHttp(Address);
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = AuthToken;

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
                        Current.Username = username;
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
                WebRequest.CreateHttp(Address);
            request.Accept = _type;
            request.Method = _method;
            request.ContentType = _type;
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = AuthToken;

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
                        Current.SkypeName = skype;
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
