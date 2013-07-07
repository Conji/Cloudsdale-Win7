using System;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

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

        public MessageList()

        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 90;
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
                ScreenName = "Conji";
                Username = "Conji";
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
                e.Graphics.DrawString(TimeStamp, e.Font, Brushes.DarkGray, e.Bounds.Width - 50, e.Bounds.Top);
                var textRectangle = e.Bounds;
                textRectangle.X += 70;
                textRectangle.Width -= 40;



                string itemText = Message;
                TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRectangle, e.ForeColor, flags);
                e.DrawFocusRectangle();
            }
        }
    }
}
