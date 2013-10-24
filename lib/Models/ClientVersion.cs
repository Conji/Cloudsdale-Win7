using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using CloudsdaleWin7.Views.Misc;

namespace CloudsdaleWin7.lib.Models
{
    class ClientVersion
    {
        private const string Version= "2.0.8 BETA";

        public async static Task<string> UpdatedVersion()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync(Endpoints.VersionAddress);
                return response.Trim();
            }
            catch (Exception ex)
            {
                App.Connection.NotificationController.Notification.Notify(ex.Message);
                return "UPDATE FAILED";
            }
        }
        public async static void CheckVersion()
        {
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
            MainWindow.Instance.Hide();
            MainWindow.Instance.ShowInTaskbar = false;
            Updater.Instance.Show();
            File.Move(CloudsdaleSource.ExeFile, CloudsdaleSource.OldFile);
            var client = new WebClient();
            client.DownloadFile(Endpoints.ClientAddress, CloudsdaleSource.ExeFile);
            Updater.Instance.Hide();
            Process.Start(CloudsdaleSource.ExeFile);
            MainWindow.Instance.Close();
        }
        public static void CleanUp()
        {
            try
            {
                File.Delete(CloudsdaleSource.OldFile);
            }catch(Exception e){Console.WriteLine(e.Message);}
        }
    }
}
