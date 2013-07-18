using System.IO;
using System.Drawing;
using System.Net;

namespace Cloudsdale.lib.Models
{
    public class AvatarConverter
    {
        public static int Normal = 200;
        public static int Mini = 24;
        public static int Thumb = 50;
        public static int Preview = 70;
        public static int Chat = 40;

        public AvatarConverter(string size){}
        public static Image LoadImage(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            Bitmap ReturnedImage = new Bitmap(responseStream);
            responseStream.Dispose();

            return ReturnedImage;
        }
    }
}
