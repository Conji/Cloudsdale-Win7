using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudsdaleWin7.lib.Controllers
{
    public class NotificationController
    {
        public NotifyIcon Notification = new NotifyIcon();

        public void Notify(string title, string text)
        {
            Notification.BalloonTipIcon = ToolTipIcon.Info;
            Notification.BalloonTipText = text;
            Notification.BalloonTipTitle = title;
            Notification.ShowBalloonTip(2000000);
        }
    }
}
