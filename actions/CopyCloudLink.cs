using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cloudsdale;
using Cloudsdale.connection;
using Cloudsdale.lib;
using Newtonsoft.Json.Linq;

namespace Cloudsdale.actions
{
    class CopyCloudLink
    {
        public static void FetchCloudLink(ICollection<JToken> clouds)
        {
            Main instance = new Main();
            var Index = instance.CloudList.SelectedIndices.ToString();
            foreach (var cloud in clouds)
                if (instance.CloudList.FocusedItem.Selected.ToString() != null)
                {
                    if (cloud["name"].ToString() == instance.CloudList.FocusedItem.ToString())
                    {
                        Clipboard.SetText(Endpoints.Cloud.Replace("[:id]", cloud["id"].ToString()));
                        MessageBox.Show("Link copied to clipboard!");
                    }
                }
                else
                {
                    MessageBox.Show("Could not fetch link!");
                }
        }
    }
}
