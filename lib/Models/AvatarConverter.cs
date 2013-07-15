using System.IO;
using System.Drawing;
using System.Net;

namespace Cloudsdale.lib.Models
{
    class AvatarConverter
    {
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
