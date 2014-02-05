using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Win = System.Windows.Forms;

namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc.Screenshot
{
    public static class Screencap
    {
        public static string SavedDirectory = String.Format("C:\\Users\\{0}\\Pictures\\Cloudsdale\\", Environment.UserName);

        public static Bitmap CapChat()
        {
            var topLeftX = int.Parse((Main.Instance.Frame.PointToScreen(new System.Windows.Point()).X).ToString().Split('.')[0]);
            var topLeftY = int.Parse((Main.Instance.Frame.PointToScreen(new System.Windows.Point()).Y).ToString().Split('.')[0]);

            var b = new Bitmap((int)Main.Instance.Frame.ActualWidth, (int)Main.Instance.Frame.ActualHeight, PixelFormat.Format32bppPArgb);
            var s = Graphics.FromImage(b);

            s.CopyFromScreen(topLeftX,
                            topLeftY,
                            0,
                            0,
                            new System.Drawing.Size((int)Main.Instance.Frame.ActualWidth, (int)Main.Instance.Frame.ActualHeight),
                            CopyPixelOperation.SourceCopy);

            return b;
        }

        public static bool DoCap()
        {
            var ran = SavedDirectory + DateTime.Now.ToString("yyyyMMddHHmmss").FormatToFile();
            CapChat().Save(ran, ImageFormat.Png);
            var v = new ViewCapture(ran);
            v.Show();
            CapChat().Dispose();

            return true;
        }

        public static async Task<bool> UploadCap(byte[] image)
        {
            return await new Task<bool>(() =>
                                        {
                                            var boundary = "----CDAppBoundary" + Guid.NewGuid();
                                            byte[] data;
                                            using (var dataStream = new MemoryStream())
                                            {
                                                dataStream.Write(Encoding.UTF8.GetBytes("--" + boundary), 0, Encoding.UTF8.GetByteCount("--" + boundary));
                                                dataStream.Write(Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"image\"; filename=\"" +
                                                                                        Uri.EscapeDataString(ViewCapture.Instance.Location.Split('\\')[ViewCapture.Instance.Location.Split('\\').Length - 1]) + ".jpg\""), 0, Encoding.UTF8.GetByteCount("Content-Disposition: form-data; name=\"image\"; filename=\"" +
                                                                                                                                                                                                                                                         Uri.EscapeDataString(ViewCapture.Instance.Location.Split('\\')[ViewCapture.Instance.Location.Split('\\').Length - 1]) + ".jpg\""));
                                                dataStream.Write(Encoding.UTF8.GetBytes("Content-Type: image/jpeg"), 0, Encoding.UTF8.GetByteCount("Content-Type: image/jpeg"));
                                                dataStream.Write(image, 0, image.Length);
                                                dataStream.Write(Encoding.UTF8.GetBytes("--" + boundary + "--"), 0, Encoding.UTF8.GetByteCount("--" + boundary + "--"));

                                                data = dataStream.ToArray();
                                            }

                                            var client = new HttpClient().AcceptsJson();
                                            client.DefaultRequestHeaders.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
                                            client.DefaultRequestHeaders.Add("Content-Length", data.Count().ToString());

                                            var response = JObject.Parse(client.PostAsync("http://imm.io/store/", new ByteArrayContent(data)).Result.Content.ReadAsStringAsync().Result);
                                            Console.WriteLine(response["success"]);
                                            return true;
                                        });
        }
    }
}
