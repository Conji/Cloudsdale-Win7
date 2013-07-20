using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Cloudsdale.lib.Controllers.MessageController.Processors
{
    class QuoteProcessor
    {
        public static Color Process(string message, Label messageText)
        {
            if (message.StartsWith(">") && !message.StartsWith(">.") && !message.StartsWith(">:"))
            {
                messageText.ForeColor = Color.LimeGreen;
            }
            return messageText.ForeColor;
        }
    }
}
