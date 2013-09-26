using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Helpers;

namespace CloudsdaleWin7.Controls
{
    class BindableLink : Hyperlink
    {
        public string NavigateOnClick { get; set; }

        public BindableLink()
        {
            Click += DoOnClick;
        }
        private void DoOnClick(object sender, EventArgs e)
        {
            BrowserHelper.FollowLink(NavigateOnClick.AssuredLink());
        }
    }
}
