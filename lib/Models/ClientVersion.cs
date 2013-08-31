using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using CloudsdaleWin7.lib.ErrorConsole.CConsole;

namespace CloudsdaleWin7.lib.Models
{
    class ClientVersion
    {
        public static string VERSION= "1.6.3 BETA";

        public static string UpdatedVersion()
        {
            try
            {
                var request = WebRequest.CreateHttp(Endpoints.VersionAddress);
                var responseStream = request.GetResponse().GetResponseStream();
                var response = new StreamReader(responseStream);
                return response.ReadToEnd().Trim();
            }catch (Exception ex)
            {
                WriteError.ShowError(ex.Message);
                return "UPDATED FAILED";
            }
        }
        public static void CheckVersion()
        {
            //fix this
            if (UpdatedVersion() == "UPDATE FAILED") return;
            if (UpdatedVersion() == VERSION && UpdatedVersion() != "UPDATE FAILED") return;
            if (MessageBox.Show("A new version is available. Would you like to update?", "Cloudsdale Updater",
                                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                UpdateClient();
            }
        }
        private static void UpdateClient()
        {
            
            File.Move(CloudsdaleSource.File, CloudsdaleSource.Folder + "old.file");
            var client = new WebClient();
            client.DownloadFile(Endpoints.ClientAddress, CloudsdaleSource.File);
            Process.Start(CloudsdaleSource.File);
            MainWindow.Instance.Close();
        }
        public static void CleanUp()
        {
            try
            {
                File.Delete(CloudsdaleSource.Folder + "old.file");
            }catch{}
        }
    }
}
