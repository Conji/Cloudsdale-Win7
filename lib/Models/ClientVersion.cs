using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using CloudsdaleWin7.lib.ErrorConsole.CConsole;

namespace CloudsdaleWin7.lib.Models
{
    class ClientVersion
    {
        private const string Version= "2.0.6 BETA";

        public async static Task<string> UpdatedVersion()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync(Endpoints.VersionAddress);
                return response.Trim();
            }catch (Exception ex)
            {
                WriteError.ShowError(ex.Message);
                return "UPDATE FAILED";
            }
        }
        public async static void CheckVersion()
        {
            //fix this
            if (await UpdatedVersion() == Version || await UpdatedVersion() == "UPDATE FAILED") return;
            if (MessageBox.Show("A new version is available. Would you like to update?", "Cloudsdale Updater",
                                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                UpdateClient();
            }
        }

        public static void Validate()
        {
            CheckVersion();
            CleanUp();
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
            }catch(Exception e){Console.WriteLine(e.Message);}
        }
    }
}
