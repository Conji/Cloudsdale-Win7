using System.Windows.Controls;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.CloudViews
{
    /// <summary>
    /// Interaction logic for BannedCloud.xaml
    /// </summary>
    public partial class BannedCloud
    {
        private string sId { get; set; }
        public BannedCloud(Ban ban, string id)
        {
            InitializeComponent();
            sId = id;
            CloudName.Text = App.Connection.ModelController.Clouds[id].Name;
            BanDue.Text = ban.Due.Value.ToShortDateString();
            BanReason.Text = "\"" + ban.Reason + "\"";
            CloudRules.Text = App.Connection.MessageController.CurrentCloud.Cloud.Rules != null
                ? App.Connection.MessageController.CurrentCloud.Cloud.Rules.Replace("\n", "\r\n")
                : "";
        }

        private void LeaveCloud(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Connection.ModelController.Clouds[sId].Leave();
        }
    }
}
