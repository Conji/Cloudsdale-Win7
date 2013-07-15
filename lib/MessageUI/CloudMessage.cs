using System;
using System.Drawing;
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

        public CloudMessage(string name, string user, string role, string time, string status, string device,
                            string content)
        {
            InitializeComponent();
        }

        public CloudMessage(string name, string user, string role, string time, string status, string device,
                            string content, Image avatar)
        {
            InitializeComponent();

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                                      {
                                          _name = name;
                                          _username = user;
                                          _role = role;
                                          _time = time;
                                          _status = status;
                                          _platform = device;
                                          _content = content;
                                          _avatar = avatar;

                                          c_name.Text = SetName;
                                          c_user.Text = "@" + SetUsername;
                                          switch (role)
                                          {
                                              case "founder":
                                                  c_role.BackColor = Assets.FounderTag;
                                                  c_role.BorderStyle = BorderStyle.FixedSingle;
                                                  c_role.Text = "founder";
                                                  break;
                                              case "dev":
                                                  c_role.BackColor = Assets.DevTag;
                                                  c_role.BorderStyle = BorderStyle.FixedSingle;
                                                  c_role.Text = "dev";
                                                  break;
                                              case "admin":
                                                  c_role.BackColor = Assets.AdminTag;
                                                  c_role.BorderStyle = BorderStyle.FixedSingle;
                                                  c_role.Text = "admin";
                                                  break;
                                              case "donator":
                                                  c_role.BackColor = Color.Goldenrod;
                                                  c_role.BorderStyle = BorderStyle.FixedSingle;
                                                  c_role.Text = "donator";
                                                  break;
                                              case "legacy":
                                                  c_role.BackColor = Color.Gray;
                                                  c_role.BorderStyle = BorderStyle.FixedSingle;
                                                  c_role.Text = "legacy";
                                                  break;
                                              default:
                                                  c_role.BackColor = Assets.PrimaryBackground;
                                                  c_role.BorderStyle = BorderStyle.None;
                                                  c_role.Text = "";
                                                  break;
                                          }
                                          c_time.Text = SetTimestamp;
                                          switch (SetRole)
                                          {
                                              case "online":
                                                  c_status.FillColor = Assets.OnlineStatus;
                                                  break;
                                              case "offline":
                                                  c_status.FillColor = Assets.OfflineStatus;
                                                  break;
                                              case "busy":
                                                  c_status.FillColor = Assets.BusyStatus;
                                                  break;
                                              case "away":
                                                  c_status.FillColor = Assets.AwayStatus;
                                                  break;
                                              default:
                                                  c_status.FillColor = Assets.OfflineStatus;
                                                  break;
                                          }
                                          switch (SetPlatform)
                                          {
                                              case "mobile":
                                                  c_platform.Image = Properties.Resources.phone;
                                                  break;
                                          }
                                          if (_content.StartsWith(">") && !_content.StartsWith(">.>") &&
                                              !_content.StartsWith(">:"))
                                          {
                                              c_content.Text = SetContent;
                                              c_content.ForeColor = Assets.Success_Bright;
                                          }
                                          else if (_content.StartsWith("/me "))
                                          {
                                              c_content.Text = SetContent.Replace("/me", _name);
                                          }
                                          else
                                          {
                                              c_content.Text = SetContent;
                                          }
                                          c_avatar.Image = Properties.Resources.user;
                                      }));
            }


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
            set { c_user.Text = GetUsername("@" + _username); }
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
    }
}
