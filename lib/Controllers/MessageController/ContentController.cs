using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloudsdale.lib.Controllers.MessageController.Processors;

namespace Cloudsdale.lib.Controllers.MessageController
{
    class ContentController
    {
        public static string ProcessedContent(string message, string userId)
        {
            message = SlashMeProcessor.Process(message, userId);
            //quotation conversion
            return message;
        }
    }
}
