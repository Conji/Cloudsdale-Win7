using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloudsdale.lib.Controllers.MessageController.Processors
{
    class DropProcessor
    {
        public static void AddDrop(string message, string userId)
        {
            var messageParts = message.Split(' ');
            Console.WriteLine(messageParts);
        }
    }
}
