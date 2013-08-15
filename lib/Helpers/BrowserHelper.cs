using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsdaleWin7.lib.Helpers
{
    public class BrowserHelper
    {
        private static bool IsCloudLink(string link)
        {
            if (link.StartsWith("http://www.cloudsdale.org/clouds/") || link.StartsWith("www.cloudsdale.org/clouds/"))
            {
                return true;
            }
            return false;
        }
        public static void ViewInBrowser(string uri)
        {
            if (!IsCloudLink(uri))
            {
                MainWindow.Instance.Frame.Navigate(new Browser());
                Browser.Instance.WebAddress.Text = uri;
                Browser.Instance.WebBrowser.Navigate(uri);
            }else
            {
                //add cloud to list
            }
        }
    }
}
