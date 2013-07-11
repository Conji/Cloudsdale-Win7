using System;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Cloudsdale.actions.MessageController;
using Cloudsdale.connection;

namespace Cloudsdale.lib.MessageUI
{

    class MessageList : ListBox
    {
        public static bool HasTag;
        public static string Username;
        public static string ScreenName;
        public static string TimeStamp;
        public static string Platform;
        public static Image Avatar;
        public static string Message;
        public static string UserTag;
        public static string UserStatus;
        public MessageSource CloudMessages = new MessageSource();

        public MessageList() 
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 110;
            this.DoubleBuffered = true;
            this.ScrollAlwaysVisible = true;
            this.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.BackColor = Assets.PrimaryBackground;
            
        }
        public static void AddMessage(string _name, string _username, Image _avatar, string _status, string _timestamp, string _platform, string _content)
        {

        }
 
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            e.DrawBackground();
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

            if (e.Index >= 0)
            {
                Avatar = Cloudsdale.Properties.Resources.user;
                HasTag = true;
                UserTag = "default";
                ScreenName = "ScreenName"; 
                Username = "Username";
                Platform = "device";
                Message = "user message";
                UserStatus = "Offline";
                TimeStamp = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
                e.DrawBackground();
                e.Graphics.DrawRectangle(Pens.Black, e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
                //Draw user's image
                e.Graphics.DrawImage(Avatar, e.Bounds.X + 2, e.Bounds.Y + 10, 64, 64);
                //Draw user's tag if it exists
                if (HasTag == true) { e.Graphics.DrawString(UserTag, e.Font, Brushes.Black, e.Bounds.X + 2, e.Bounds.Y + 72); }
                //Draw user's name
                e.Graphics.DrawString(ScreenName + "    @" + Username, e.Font, Brushes.Black, e.Bounds.X + 80, e.Bounds.Y + 10);
                //Draw message time
                e.Graphics.DrawString(TimeStamp, e.Font, Brushes.DarkGray, e.Bounds.Width - 60, e.Bounds.Top);
                //Draw user platform
                Image DeviceIcon;
                switch (Platform)
                {
                    case "mobile":
                        DeviceIcon = Cloudsdale.Properties.Resources.phone;
                        e.Graphics.DrawImage(DeviceIcon, e.Bounds.X + 27, e.Bounds.Y + 90, 12, 16);
                        break;
                    case "robot":
                        DeviceIcon = Cloudsdale.Properties.Resources.cog;
                        e.Graphics.DrawImage(DeviceIcon, e.Bounds.X + 27, e.Bounds.Y + 90, 16, 16);
                        break;
                    default: 
                        break;
                }
                //Draw user status

                Brush online = new SolidBrush(Assets.Success_Bright);
                Brush offline = new SolidBrush(Color.Gray);
                Brush busy = new SolidBrush(Assets.Error_Bright);
                Brush away = new SolidBrush(Color.Yellow);

                Brush B1;

                switch (UserStatus)
                {
                    case "Online":
                        B1 = online;
                        e.Graphics.FillEllipse(B1, e.Bounds.X + 70, e.Bounds.Y + 17, 8, 8);
                        break;
                    case "Offline":
                        B1 = offline;
                        e.Graphics.FillEllipse(B1, e.Bounds.X + 70, e.Bounds.Y + 17, 8, 8);
                        break;
                    case "Busy":
                        B1 = busy;
                        e.Graphics.FillEllipse(B1, e.Bounds.X + 70, e.Bounds.Y + 17, 8, 8);
                        break;
                    case "Away" :
                        B1 = away;
                        e.Graphics.FillEllipse(B1, e.Bounds.X + 70, e.Bounds.Y + 17, 8, 8);
                        break;
                    default:
                        break;
                }
                

                var textRectangle = e.Bounds;
                textRectangle.X += 70;
                textRectangle.Width -= 40;

                TextRenderer.DrawText(e.Graphics, Message, e.Font, textRectangle, e.ForeColor, flags);
                e.DrawFocusRectangle();
            }
        }
    }
}
