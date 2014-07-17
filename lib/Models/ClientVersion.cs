using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CloudsdaleWin7.Views.Misc;

namespace CloudsdaleWin7.lib.Models
{
    public class ClientVersion
    {
        public const string Version = "1.0.0 RELEASE";

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
        private async static void UpdateClient()
        {
            MainWindow.Instance.Hide();
            var win = new UpdatingWindow();
            win.Show();
            File.Move(CloudsdaleSource.ExeFile, CloudsdaleSource.OldFile);
            var client = new WebClient();
            
            await Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                client.DownloadFile(new Uri(Endpoints.ClientAddress), CloudsdaleSource.ExeFile);
                win.Close();
                MainWindow.Instance.Close();
            });
            Process.Start(CloudsdaleSource.ExeFile);
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
