using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CloudsdaleWin7.lib.Models
{
    public class ClientVersion
    {
        public const string Version = "1.0.0.2 PRE-RELEASE";

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
                return "0";
            }
        }
        public async static void CheckVersion()
        {
            if (await UpdatedVersion() == Version || await UpdatedVersion() == "0") return;
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
            MessageBox.Show("Please wait while we update. The window will close once it's finished.");
            MainWindow.Instance.Width = 50;
            MainWindow.Instance.Height = 20;
            MainWindow.Instance.IsEnabled = false;
            MainWindow.Instance.ShowInTaskbar = false;
            File.Move(CloudsdaleSource.ExeFile, CloudsdaleSource.OldFile);
            var client = new WebClient();
            client.DownloadFile(Endpoints.ClientAddress, CloudsdaleSource.ExeFile);
            Process.Start(CloudsdaleSource.ExeFile);
            MainWindow.Instance.Close();
        }
        public static void CleanUp()
        {
            try
            {
                File.Delete(CloudsdaleSource.OldFile);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e.Message);
#endif
                App.Connection.NotificationController.Notification.Notify(e.Message);
            }
        }
    }
}
