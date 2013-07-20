using System.Windows.Forms;
using Cloudsdale.lib.Models;

namespace Cloudsdale.lib.Controllers.CloudSubscriber
{
    class NotificationControl
    {
        private static NotifyIcon _subscriber = new NotifyIcon();
        private static Main Instance = new Main();
        public NotificationControl()
        {
            
        }
        public static void ShowSimple(string cloudId, string userId)
        {
            _subscriber.BalloonTipIcon = ToolTipIcon.None;
            _subscriber.BalloonTipText = UserModel.Name(userId) + "said something in " + CloudModel.CloudName(cloudId);
            _subscriber.ShowBalloonTip(3);
        }
        public static void ShowDetailed(string cloudId, string userId, string message)
        {
            _subscriber.BalloonTipIcon = ToolTipIcon.Info;
            _subscriber.BalloonTipText = UserModel.Name(userId) + "(@" + UserModel.Username(userId) + "): " + message;
            _subscriber.BalloonTipTitle = CloudModel.CloudName(cloudId);
        }
    }
}
