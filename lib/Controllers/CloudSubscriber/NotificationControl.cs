using System.Windows.Forms;
using Cloudsdale.lib.Models;

namespace Cloudsdale.lib.Controllers.CloudSubscriber
{
    class NotificationControl
    {
        private static Main Instance = new Main();
        public NotificationControl()
        {
            
        }
        public static void ShowSimple(string cloudId, string userId)
        {
            Instance.Subscriber.BalloonTipIcon = ToolTipIcon.None;
            Instance.Subscriber.BalloonTipText =
                "[:user] said something in [:cloud]!".Replace("[:user]", UserModel.Name(userId)).Replace("[:cloud]",
                                                                                                         CloudModel.
                                                                                                             CloudName(
                                                                                                                 cloudId));
            Instance.Subscriber.ShowBalloonTip(3);
        }
        public static void ShowDetailed(string cloudId, string userId, string message)
        {
            Instance.Subscriber.BalloonTipIcon = ToolTipIcon.Info;
            Instance.Subscriber.BalloonTipText = "[:name](@[:username]):".Replace("[:name]", 
                UserModel.Name(userId)).Replace("[:username]", 
                UserModel.Username(userId)) + message;
            Instance.Subscriber.BalloonTipTitle = CloudModel.CloudName(cloudId);
            Instance.Subscriber.ShowBalloonTip(3);
        }
    }
}
