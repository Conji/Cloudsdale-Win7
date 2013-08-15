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
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.Views
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        public static UserInfo Instance;
        public UserInfo()
        {
            InitializeComponent();
            Instance = this;
        }

        public void ShowUserInfo(JObject userObject)
        {
            var id = userObject["id"].ToString();
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
            if (BanInfo.Height == 0)
            {
                BanInfo.Height = 70;
            }else
            {
                //ban user
                BanInfo.Height = 0;
            }
        }
        private static JObject BaseObject(string id)
        {
            var request = WebRequest.CreateHttp(Endpoints.UserJson.Replace("[:id]", id));
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var responseReader = new StreamReader(responseStream);
            var responseData = JObject.Parse(responseReader.ReadToEnd());
            return (JObject)responseData["result"];
        }
    }
}
