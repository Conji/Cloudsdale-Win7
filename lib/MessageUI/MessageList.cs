using System;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Cloudsdale.actions.MessageController;

namespace Cloudsdale.lib.MessageUI
{
    class MessageList : ListBox
    {
        public bool HasTag;
        public string Username;
        public string ScreenName;
        public string TimeStamp;
        public string Platform;
        public Image Avatar;
        public string Message;
        public string UserTag;
        public string UserStatus;

        public MessageList()

        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 110;
            this.DoubleBuffered = true;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                Point F1 = new Point(this.Bounds.Left, this.Bounds.Bottom);
                Point F2 = new Point(this.Bounds.Left, this.Bounds.Top);
                Brush grad_fill = new LinearGradientBrush(F1, F2, Assets.PrimaryBackground, Assets.InnerBackground);
                base.BackColor = new Pen(grad_fill).Color;
            }
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
                UserStatus = "Online";
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
                Pen online = new Pen(Assets.Success_Bright);
                Pen offline = new Pen(Color.Gray);
                Pen Busy = new Pen(Assets.Error_Bright);
                Pen Away = new Pen(Color.Yellow);
                Pen P1;
                switch (UserStatus)
                {
                    case "Online":
                        P1 = online;
                        e.Graphics.DrawPie(P1, e.Bounds.X + 70, e.Bounds.Y - 20, 5, 5, 0, 180);
                        break;
                    case "Offline":
                        P1 = offline;
                        e.Graphics.DrawPie(P1, e.Bounds.X + 70, e.Bounds.Y - 20, 5, 5, 0, 180);
                        break;
                    case "Busy":
                        P1 = Busy;
                        e.Graphics.DrawPie(P1, e.Bounds.X + 70, e.Bounds.Y - 20, 5, 5, 0, 180);
                        break;
                    case "Away" :
                        P1 = Away;
                        e.Graphics.DrawPie(P1, e.Bounds.X + 70, e.Bounds.Y - 20, 5, 5, 0, 180);
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
