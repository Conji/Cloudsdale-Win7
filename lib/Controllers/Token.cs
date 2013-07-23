using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cloudsdale.lib;

namespace Cloudsdale.lib.Controllers
{
    class Token
    {
        public static void SaveToken(string X_Auth_Token)
        {
            if (!File.Exists(Assets.DataDirectory + "session.json"))
            {
                File.Create(Assets.DataDirectory + "session.json");
            }
            File.WriteAllText(Assets.DataDirectory + "session.json", string.Empty);
            File.AppendAllText(Assets.DataDirectory + "session.json", X_Auth_Token);
        }
        public static string ReadToken()
        {
            string ReadResponse;
            try
            {
                var reader = new StreamReader(Assets.DataDirectory + "session.json");
                var response = reader.ReadToEnd();
                ReadResponse = response;
                reader.Close();

            }
            catch(Exception ex)
            {
                ReadResponse = ex.Message;
            }
            return ReadResponse;
        }
    }
}
