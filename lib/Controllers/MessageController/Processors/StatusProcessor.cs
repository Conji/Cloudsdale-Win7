using System.Drawing;
using Cloudsdale.lib.Models;

namespace Cloudsdale.lib.Controllers.MessageController.Processors
{
    class StatusProcessor
    {
        public static Color Process(string userId)
        {
            var _statusColor = new Color();
            switch (UserModel.Status(userId).ToLower())
            {
                case "online":
                    _statusColor = Assets.OnlineStatus;
                    break;
                case "offline":
                    _statusColor = Assets.OfflineStatus;
                    break;
                case "busy":
                    _statusColor = Assets.BusyStatus;
                    break;
                case "away":
                    _statusColor = Assets.AwayStatus;
                    break;
            }
            return _statusColor;
        }
    }
}
