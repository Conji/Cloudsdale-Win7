using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace CloudsdaleWin7.lib.Models
{
    class ClientVersion
    {
        public static string CLIENT_VERSION = "1.5 BETA";

        private static string UpdatedVersion()
        {
            var request = WebRequest.CreateHttp(Endpoints.VersionAddress);
            var responseStream = request.GetResponse().GetResponseStream();
            var response = new StreamReader(responseStream);
            return response.ReadToEnd();
        }
        public static void CheckVersion()
        {
            if (UpdatedVersion() != CLIENT_VERSION)
            {
                if (MessageBox.Show("A new version is available. Would you like to update?", "Cloudsdale Updater",
                                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    UpdateClient();
                }
            }
        }
        private static void UpdateClient()
        {
            
            //File.Move(Cloudsdale_Source.File, Cloudsdale_Source.Folder + "old.file");
            var client = new WebClient();
            client.DownloadFile(Endpoints.ClientAddress, CloudsdaleSource.File);
        }
    }
}
