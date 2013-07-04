using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloudsdale.lib
{
    class UnhandledSwagEvent : Exception
    {

        private string UserMessage;

        public override string Message
        {
            get
            {
                return "Error detected! Not enough swag detected! Please format your local hard-drive to fix error.";
            }
        }
    }
}
