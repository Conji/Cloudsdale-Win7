namespace Cloudsdale.lib.Controllers.MessageController.Processors
{
    class DropProcessor
    {
        public static void AddDrop(string message, string userId)
        {
            if (message.Contains("http://www."))
            {
                var htmlLink = message.Substring(message.IndexOf("http"));
                
            }
        }
    }
}
