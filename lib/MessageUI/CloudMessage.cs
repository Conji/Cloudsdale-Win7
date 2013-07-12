using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloudsdale.lib.MessageUI
{
    public partial class CloudMessage : UserControl
    {
        public string _name { get; set; }
        public string _username { get; set; }
        public string _role { get; set; }
        public string _time { get; set; }
        public string _status { get; set; }
        public string _platform { get; set; }
        public string _content { get; set; }
        public Image _avatar { get; set; }

        public CloudMessage()
        {
            InitializeComponent();
        }
        #region Setting up the message
        #region Screen names
        public string GetName(string Screenname)
        {
            return Screenname;
        }
        public string SetName
        {
            get { return GetName(_name); }
            set { c_name.Text = GetName(_name); }
        }
        #endregion
        #region Usernames
        public string GetUsername(string Username)
        {
            return Username;
        }
        /// <summary>
        /// Sets the username of the message
        /// </summary>
        public string SetUsername
        {
            get { return GetUsername(_username); }
            set { c_user.Text = GetUsername(_username); }
        }
        #endregion
        #region User role
        public string GetRole(string role)
        {
            return role;
        }
        /// <summary>
        /// Sets the role for the message
        /// </summary>
        public string SetRole
        {
            get { return GetRole(_role); }
            set
            {
                switch (GetRole(_role))
                {
                    case "founder":
                        value = "founder";
                        break;
                    case "dev":
                        value = "dev";
                        break;
                    case "admin":
                        value = "admin";
                        break;
                    case "donator":
                        value = "donator";
                        break;
                    case "legacy":
                        value = "legacy";
                        break;
                    default:
                        value = "default";
                        break;
                }
            }
        }
        #endregion
        #region Timestamp
        public string GetTimestamp(string time)
        {
            return time;
        }
        /// <summary>
        /// Sets the timestamp for the new message
        /// </summary>
        public string SetTimestamp
        {
            get { return GetTimestamp(_time); }
            set { c_time.Text = GetTimestamp(_time); }
        }
        #endregion
        #region status
        public string GetStatus(string status)
        {
            return status;
        }
        /// <summary>
        /// gets the status of the user sending the message
        /// </summary>
        public string SetStatus
        {
            get { return GetStatus(_status); }
            set
            {
                switch (GetStatus(_status))
                {
                    case "online":
                        value = "online";
                        break;
                    case "offline":
                        value = "offline";
                        break;
                    case "away":
                        value = "away";
                        break;
                    case "busy":
                        value = "busy";
                        break;
                    default:
                        value = "offline";
                        break;
                }
            }
        }
        #endregion
        #region Device
        public string GetPlatform(string platform)
        {
            return platform;
        }
        /// <summary>
        /// Sets the device of the user's message
        /// </summary>
        public string SetPlatform
        {
            get { return GetPlatform(_platform); }
            set
            {
                switch (GetPlatform(_platform))
                {
                    case "mobile":
                        //c_platform.Image = Cloudsdale.Properties.Resources.phone;
                        value = "mobile";
                        break;
                    case "desktop":
                        value = "desktop";
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        #region Message
        public string GetContent(string content)
        {
            return content;
        }
        /// <summary>
        /// Sets the content of the message (content being the actual message)
        /// </summary>
        public string SetContent
        {
            get { return GetContent(_content); }
            set { c_content.Text = GetContent(_content); }
        }
        #endregion
        #region Avatar
        public Image GetAvatar(Image avatar)
        {
            return avatar;
        }
        /// <summary>
        /// Sets the avatar of the message
        /// </summary>
        public Image SetAvatar
        {
            get { return GetAvatar(_avatar); }
            set { c_avatar.Image = GetAvatar(_avatar); }
        }
        #endregion
        #endregion
        public static CloudMessage NewMessage = new CloudMessage();
        public void AddMessage(ListView cloud, string name, string username, string role, string time, string status, string platform, string content)
        {

            NewMessage.Width = cloud.Width;

            NewMessage._name = name;
            NewMessage._username = username;
            NewMessage._role = role;
            NewMessage._time = time;
            NewMessage._status = status;
            NewMessage._platform = platform;
            NewMessage._content = content;
            NewMessage._avatar = Cloudsdale.Properties.Resources.user;

            NewMessage.c_name.Text = NewMessage.SetName;
            NewMessage.c_user.Text = NewMessage.SetUsername;
            #region c_role = SetRole;
            switch (NewMessage.SetRole)
            {
                case "founder":
                    NewMessage.c_role.Text = "founder";
                    NewMessage.c_role.BackColor = Assets.FounderTag;
                    break;
                case "dev":
                    NewMessage.c_role.Text = "dev";
                    NewMessage.c_role.BackColor = Assets.DevTag;
                    break;
                case "admin":
                    NewMessage.c_role.Text = "admin";
                    NewMessage.c_role.BackColor = Assets.AdminTag;
                    break;
                case "donator":
                    NewMessage.c_role.Text = "donator";
                    NewMessage.c_role.BackColor = Color.Goldenrod;
                    break;
                case "legacy":
                    NewMessage.c_role.Text = "legacy";
                    NewMessage.c_role.BackColor = Color.LightGray;
                    break;
                default:
                    NewMessage.c_role.Text = null;
                    NewMessage.c_role.BackColor = Assets.PrimaryBackground;
                    NewMessage.c_role.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    break;
            }
            #endregion
            NewMessage.c_time.Text = NewMessage.SetTimestamp;
            #region c_status = SetStatus;
            switch (NewMessage.SetRole)
            {
                case "online":
                    NewMessage.c_status.FillColor = Assets.OnlineStatus;
                    break;
                case "offline":
                    NewMessage.c_status.FillColor = Assets.OfflineStatus;
                    break;
                case "away":
                    NewMessage.c_status.FillColor = Assets.AwayStatus;
                    break;
                case "busy":
                    NewMessage.c_status.FillColor = Assets.BusyStatus;
                    break;
                default:
                    break;
            }
            #endregion
            #region c_platform = SetPlatform;
            switch (NewMessage.SetPlatform)
            {
                case "mobile":
                    NewMessage.c_platform.Image = Cloudsdale.Properties.Resources.phone;
                    break;
                default:
                    NewMessage.c_platform.Image = null;
                    break;
            }
            #endregion
            if (NewMessage._content.StartsWith(">") && !NewMessage._content.StartsWith(">.>") && !NewMessage._content.StartsWith(">:"))
            {
                NewMessage.c_content.Text = NewMessage.SetContent;
                NewMessage.c_content.ForeColor = Assets.Success_Bright;
            }
            else if (NewMessage._content.StartsWith("/me "))
            {
                NewMessage.c_content.Text = NewMessage.SetContent.Replace("/me", NewMessage._name);
            }
            else { NewMessage.c_content.Text = NewMessage.SetContent; }
            NewMessage.c_avatar.Image = NewMessage.SetAvatar;

            cloud.Controls.Add(NewMessage);
        }
        public void AddMessage(ListView cloud, string name, string username, string role, string time, string status, string platform, string content, Image avatar)
        {

            NewMessage.Width = cloud.Width;

            NewMessage._name = name;
            NewMessage._username = username;
            NewMessage._role = role;
            NewMessage._time = time;
            NewMessage._status = status;
            NewMessage._platform = platform;
            NewMessage._content = content;
            NewMessage._avatar = avatar;

            NewMessage.c_name.Text = NewMessage.SetName;
            NewMessage.c_user.Text = NewMessage.SetUsername;
            switch (NewMessage.SetRole)
            {
                case "founder":
                    NewMessage.c_role.Text = "founder";
                    NewMessage.c_role.BackColor = Assets.FounderTag;
                    break;
                case "dev":
                    NewMessage.c_role.Text = "dev";
                    NewMessage.c_role.BackColor = Assets.DevTag;
                    break;
                case "admin":
                    NewMessage.c_role.Text = "admin";
                    NewMessage.c_role.BackColor = Assets.AdminTag;
                    break;
                case "donator":
                    NewMessage.c_role.Text = "donator";
                    NewMessage.c_role.BackColor = Color.Goldenrod;
                    break;
                case "legacy":
                    NewMessage.c_role.Text = "legacy";
                    NewMessage.c_role.BackColor = Color.LightGray;
                    break;
                default:
                    NewMessage.c_role.Text = null;
                    NewMessage.c_role.BackColor = Assets.PrimaryBackground;
                    NewMessage.c_role.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    break;
            }
            NewMessage.c_time.Text = NewMessage.SetTimestamp;
            #region c_status = SetStatus;
            switch (NewMessage.SetRole)
            {
                case "online":
                    NewMessage.c_status.FillColor = Assets.OnlineStatus;
                    break;
                case "offline":
                    NewMessage.c_status.FillColor = Assets.OfflineStatus;
                    break;
                case "away":
                    NewMessage.c_status.FillColor = Assets.AwayStatus;
                    break;
                case "busy":
                    NewMessage.c_status.FillColor = Assets.BusyStatus;
                    break;
                default:
                    break;
            }
            #endregion
            #region c_platform = SetPlatform;
            switch (NewMessage.SetPlatform)
            {
                case "mobile":
                    NewMessage.c_platform.Image = Cloudsdale.Properties.Resources.phone;
                    break;
                default:
                    NewMessage.c_platform.Image = null;
                    break;
            }
            #endregion
            if (NewMessage._content.StartsWith(">") && !NewMessage._content.StartsWith(">.>") && !NewMessage._content.StartsWith(">:"))
            {
                NewMessage.c_content.Text = NewMessage.SetContent;
                NewMessage.c_content.ForeColor = Assets.Success_Bright;
            }
            else if (NewMessage._content.StartsWith("/me "))
            {
                NewMessage.c_content.Text = NewMessage.SetContent.Replace("/me", Main.User["user"]["name"].ToString());
            }
            else { NewMessage.c_content.Text = NewMessage.SetContent; }
            NewMessage.c_avatar.Image = NewMessage.SetAvatar;

            cloud.Controls.Add(NewMessage);
        }
    }
}
