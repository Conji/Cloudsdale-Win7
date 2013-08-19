using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Notifications;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.Views
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        public static UserInfo Instance;
        public static string _id { get; set; }
        public UserInfo()
        {
            InitializeComponent();
            Instance = this;
        }

        public void ShowUserInfo(JObject userObject)
        {
            var id = userObject["id"].ToString();
            _id = id;
            var name = userObject["name"].ToString();
            var username = "@" + userObject["username"].ToString();
            var avatarUri = new Uri(userObject["avatar"]["preview"].ToString(), UriKind.Absolute);
            if (userObject["skype_name"] != null)
            {
                var skype = userObject["skype_name"].ToString();
                skypeName.Text = skype;
                skypePanel.Visibility = Visibility.Visible;
            }else
            {
                skypePanel.Visibility = Visibility.Collapsed;
            }
            Title = name;
            Name.Text = name;
            Username.Text = username;
            avatar.Source = new BitmapImage(avatarUri);
            akaList.Items.Clear();
            foreach (var aka in BaseObject(id)["also_known_as"])
            {
                akaList.Items.Add(aka);
            }

            if ((CloudView.Instance.Cloud["owner_id"].ToString() == MainWindow.User["user"]["id"].ToString() 
                || CloudView.Instance.Cloud["moderator_ids"].ToString().Contains(MainWindow.User["user"]["id"].ToString())) 
                && id != MainWindow.User["user"]["id"].ToString())
            {
                cmdBan.Visibility = Visibility.Visible;
            }else
            {
                cmdBan.Visibility = Visibility.Collapsed;
            }

            Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Reason.Text == null || Reason.Text.Trim() == "")
            {
                MessageBox.Show("Please state a reason!");
            }else
            {
                var BanModel = "{\"offender_id\":\"[:id]\", \"ban\":{\"due\":\"[:date]\", \"reason\":\"[:reason]\"}}".Replace("[:id]", _id).Replace("[:date]", BanDate.DisplayDate.ToShortDateString()).Replace("[:reason]", Reason.Text);
                var data = Encoding.UTF8.GetBytes(BanModel);
                var request = WebRequest.CreateHttp(Endpoints.CloudBan.Replace("[:id]", MainWindow.CurrentCloud["id"].ToString()));
                request.Headers["X-Auth-Token"] = MainWindow.User["user"]["auth_token"].ToString();
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Method = "POST";
                request.ContentLength = data.Length;
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
        private static JObject BaseObject(string id)
        {
            try
            {
                var request = WebRequest.CreateHttp(Endpoints.UserJson.Replace("[:id]", id));
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                var responseReader = new StreamReader(responseStream);
                var responseData = JObject.Parse(responseReader.ReadToEnd());
                return (JObject)responseData["result"];
            }catch (Exception ex)
            {
                Client.Notify(ex.Message);
                return null;
            }
        }
    }
}
