using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessageControl
{
    /// <summary>
    /// Cloudsdale MessageControl
    /// </summary>
    public partial class UserControl1
    {
        public UserControl1()
        {
            InitializeComponent();

        }
        public Image fetchavatar(Image useravatar)
        {
            return useravatar;
        }
        public Image Setavatar
        {
            get { return fetchavatar(new Image()); }
            set
            {
                //in progress 
            }
        }
        public Image Fetchdevice(Image deviceinfo)
        {
            return deviceinfo;
        }
        public Image Setdevice
        {
            get { return Fetchdevice(new Image()); }
            set
            {
                //in progress }
            }
        }
        public string Fetchname(string displayname)
        {
            return displayname;
        }
        public string Setname
        {
            get { return Fetchname("new displayname"); }
            set { DisplayName.Content = value; }
        }
                public string Fetchusername(string username)
        {
            return username;
        }
        public string Setusername
        {
            get { return Fetchusername("new username"); }
            set { Username.Content = value; }
        }
        public string Fetchrole(string role)
        {
            return role;
        }
        public string Setrole
        {
            get { return Fetchrole("new role"); }
            set { Role.Content = value; }
        }
        public string Fetchtime(string timestamp)
        {
            return timestamp;
        }
        public string Settime
        {
            get { return Fetchtime("new time"); }
            set { TimeStamp.Content = value; }
        }
        public string Fetchmessage(string message)
        {
            return message;
        }
        public string Setmessage
        {
            get { return Fetchmessage("new message"); }
            set { Message.Text = value; }
        }
    }
}