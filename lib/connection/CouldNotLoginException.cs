using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cloudsdale.lib.connection
{
    public class CouldNotLoginException : Exception
    {
        private JToken data;
        public CouldNotLoginException(string responseData)
        {
            data = JObject.Parse(responseData);
        }

        public override string Message
        {
            get
            {
                return "An error occured while attempting to log in! Check to make sure your username and password are correct!";
            }
        }
    }
}
