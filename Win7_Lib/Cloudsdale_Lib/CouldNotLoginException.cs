using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7.Cloudsdale_Lib {
    public class CouldNotLoginException : Exception {
        private JToken data;
        public CouldNotLoginException(string responseData) {
            data = JObject.Parse(responseData);
        }

        public override string Message {
            get {
                return "An error occured while trying to log you in! Make sure your password is correct! The error report is: " + data.ToString();
            }
        }
    }
}
