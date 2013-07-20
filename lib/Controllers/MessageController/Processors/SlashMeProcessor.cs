using Cloudsdale.lib.Models;

namespace Cloudsdale.lib.Controllers.MessageController.Processors
{
    public sealed class SlashMeProcessor
    {
        public static string Process(string message, string userId)
        {
            if (message.StartsWith("/me "))
            {
                message = message.Replace("/me", UserModel.Name(userId));
            }
            return message;
        }
    }
}
