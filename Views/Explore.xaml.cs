using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CloudsdaleWin7.Views.ExploreViews;
using CloudsdaleWin7.Views.ExploreViews.ItemViews;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for Explore.xaml
    /// </summary>
    public partial class Explore
    {
        public Explore()
        {
            InitializeComponent();
            LoadPreviousSource();
        }

        private void LoadPreviousSource()
        {
            switch (App.Settings["selected_source"])
            {
                case "popular":
                    ExploreFrame.Navigate(new ExplorePopular());
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "recent":
                    ExploreFrame.Navigate(new ExploreRecent());
                    CmdRecent.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "roulette":
                    ExploreFrame.Navigate(new ExploreRoulette());
                    CmdRoulette.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                default:
                    ExploreFrame.Navigate(new ExplorePopular());
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
            }
        }

        private void ChangeExploreSource(object sender, RoutedEventArgs e)
        {
            foreach (Button button in ViewPanel.Children)
            {
                button.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlue);
            }
            var senderName = ((Button) sender).Name;
            switch (senderName)
            {
                case "CmdPopular":
                    App.Settings.ChangeSetting("selected_source", "popular");
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    ExploreFrame.Navigate(new ExplorePopular());
                    break;
                case "CmdRecent":
                    App.Settings.ChangeSetting("selected_source", "recent");
                    CmdRecent.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    ExploreFrame.Navigate(new ExploreRecent());
                    break;
                case "CmdRoulette":
                    App.Settings.ChangeSetting("selected_source", "roulette");
                    CmdRoulette.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    ExploreFrame.Navigate(new ExploreRoulette());
                    break;

            }
        }

        private async void SearchForCloud(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            try
            {
                var client = new HttpClient().AcceptsJson();
                var response =
                    client.GetStringAsync(Endpoints.ExploreSearch.Replace("$", ((TextBox) sender).Text.ReplaceToQuery()));
                var objects = await JsonConvert.DeserializeObjectAsync<WebResponse<Cloud[]>>(await response);

                var list = objects.Result.Select(cloud => new ItemBasic(cloud)).ToList();

                try
                {
                    var r = client.GetStringAsync(Endpoints.Cloud.Replace("[:id]", ((TextBox) sender).Text));
                    var result = JsonConvert.DeserializeObjectAsync<WebResponse<Cloud>>(r.Result);
                    if (result.Result.Result != null)
                    {
                        list.Add(new ItemBasic(result.Result.Result));
                    }
                }
                catch
                {
                }

                ExploreFrame.Navigate(new ExploreSearch(list));
            }
            catch
            {
                App.Connection.NotificationController.Notification.Notify("It looks like Twilight can't find any books about that cloud. How about you try another?");
            }
        }
    }
}
