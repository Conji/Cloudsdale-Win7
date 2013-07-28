using System.Drawing;
using System.IO;
using System.Net;
using Cloudsdale_Win7.Assets;

namespace Cloudsdale_Win7.Models
{
    class AvatarModel
    {
        public static int Normal = 200;
        public static int Mini = 24;
        public static int Thumb = 50;
        public static int Preview = 70;
        public static int Chat = 40;

        public static Image LoadImage(string UserId, int size)
        {
            WebRequest request =
                WebRequest.Create(Endpoints.Avatar.Replace("[:type]", "user").Replace("[:id]", UserId).Replace(
                    "[:size]", size.ToString()));
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            Bitmap ReturnedImage = new Bitmap(responseStream);
            responseStream.Dispose();

            return ReturnedImage;
        }
    }
}
