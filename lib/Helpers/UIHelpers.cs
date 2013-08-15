using System;
using System.Windows;
using System.Windows.Documents;

namespace CloudsdaleWin7.lib.Helpers
{
    public static class UIHelpers
    {
        public static Hyperlink OnClickLaunch(this Hyperlink link, string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                uri = "http://" + uri;
            }

            link.Click += async delegate
            {
                if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                {
                    MessageBox.Show(uri + " is not a well formed link! Please try another.");
                }
                BrowserHelper.ViewInBrowser(uri);
            };
            return link;
        }
    }
}
