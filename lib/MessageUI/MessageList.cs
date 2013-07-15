using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cloudsdale.connection;

namespace Cloudsdale.lib.MessageUI
{
    public partial class MessageList : ListView
    {
        public string AssignedCloud;
        public MessageSource ReceivedMessages = new MessageSource();
        public CloudMessage NewMessage;

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }
        public MessageList()
        {
            BackColor = Assets.InnerBackground;
            DoubleBuffered = true;     
        }
        
        public void AddMessage(string name, string username, string role, string time, string status, string platform, string content)
        {
            NewMessage = new CloudMessage();
            NewMessage.Width = this.Width - 20;

            if (NewMessage.InvokeRequired)
            {
                NewMessage.Invoke(new Action(() =>
                {
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
                            NewMessage.c_status.FillColor = Assets.OfflineStatus;
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
                    NewMessage.c_avatar.Image = Cloudsdale.Properties.Resources.user;

                    Controls.Add(NewMessage);
                }));
            }


        }
        public void AddMessage(string name, string username, string role, string time, string status, string platform, string content, Image avatar)
        {
            NewMessage = new CloudMessage();
            NewMessage.Width = this.Width;

            if (NewMessage.InvokeRequired)
            {
                NewMessage.Invoke(new Action(() =>
                {
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

                    Controls.Add(NewMessage);
                }));
            }
        }
        public void TestAdd()
        {
            NewMessage = new CloudMessage();
            Controls.Add(NewMessage);
        }
    }
}
