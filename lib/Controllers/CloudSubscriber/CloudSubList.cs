using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Cloudsdale.lib.Models;
using Newtonsoft.Json.Linq;

namespace Cloudsdale.lib.Controllers.CloudSubscriber
{
    public sealed class CloudSubList
    {
        public static Main Instance = new Main();
        public static ObservableCollection<string> SubbedClouds = new ObservableCollection<string>();
        public static string[] CloudListString;

        public static void AddCloud(string CloudId)
        {
            if (!SubbedClouds.Contains(CloudId))
            {
                SubbedClouds.Add(CloudId);
                Instance.sublist_clouds.Items.Add(CloudModel.CloudName(CloudId));
            }
            else
            {
                throw new ValueUnavailableException("Cloud is already added to the subscribed list.");
            }
        }
        public static void RemoveCloud(string CloudId)
        {
            if (SubbedClouds.Contains(CloudId))
            {
                SubbedClouds.Remove(CloudId);
                Instance.sublist_clouds.Items.Remove(CloudModel.CloudName(CloudId));
            }
            else
            {
                throw new ValueUnavailableException("Cloud isn't in the list. Cannot remove cloud.");
            }
        }
        public static void ClearList()
        {
            if (SubbedClouds.Count > 0)
            {
                SubbedClouds.Clear();
            }
        }
        public static string GetClouds()
        {
            SubbedClouds.CopyTo(CloudListString, SubbedClouds.Count - 1);
            return CloudListString[0];
        }
        public static void ShowMessages(string cloudId, string message, string userId)
        {
            if (SubbedClouds.Contains(cloudId))
            {
                if (Instance.chkSimple.Checked)
                {NotificationControl.ShowSimple(cloudId, userId);}else{ NotificationControl.ShowDetailed(cloudId, userId, message);}
            }
        }
    }
}
