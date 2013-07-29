using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
        public static string _method = "PUT";
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
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }
        public static void Avatar(Image avatar)
        {
            
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
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }
        public static void Clouds(string clouds)
        {
             var dataObject = "{ \"user\" : { \"clouds\" : [[:clouds]]}}".Replace("[:clouds]", clouds);
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
