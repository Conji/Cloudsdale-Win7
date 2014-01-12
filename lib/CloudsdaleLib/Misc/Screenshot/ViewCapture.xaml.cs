using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc.Screenshot
{
    /// <summary>
    /// Interaction logic for ViewCapture.xaml
    /// </summary>
    public partial class ViewCapture : Window
    {
        public string Location { get; set; }
        public static ViewCapture Instance { get; set; }
        public ViewCapture(string uri)
        {
            InitializeComponent();
            Location = uri;
            Instance = this;
            Preview.Source = new BitmapImage(new Uri(uri));
            Title = uri;
            UploadCapture(this, null);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void UploadCapture(object sender, RoutedEventArgs e)
        {
            byte[] data;
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(new Uri(Location)));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            if (await Screencap.UploadCap(data))
            {
                App.Connection.NotificationController.Notification.Notify("Screencap \"" + Location.Split('\\')[Location.Split('\\').Length - 1] + "\" uploaded successfully!");
            }
        }
    }
}
