using System.Drawing;
using Cloudsdale.lib.Models;

namespace Cloudsdale.lib.Controllers.MessageController.Processors
{
    class NameProcessor
    {
        private static Color NameColor;
        public static Color Process(string userId, string cloudId)
        {
            if (userId == CloudModel.OwnerID(cloudId))
            {
                NameColor = Color.Purple;
            }
            else if (CloudModel.ModeratorIDs(cloudId).Contains(userId))
            {
                NameColor = Color.Blue;
            }
            return NameColor;
        }
    }
}
