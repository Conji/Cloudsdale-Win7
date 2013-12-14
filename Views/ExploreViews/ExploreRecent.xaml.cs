using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.Views.ExploreViews.ItemViews;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.ExploreViews
{
    /// <summary>
    /// Interaction logic for ExploreRecent.xaml
    /// </summary>
    public partial class ExploreRecent : Page
    {

        private int CurrentPage { get; set; }

        public ExploreRecent()
        {
            InitializeComponent();
            FetchPopularList();
            CurrentPage = 1;
        }

        private void FetchPopularList()
        {
            LoadNext(this, null);
        }

        private async void LoadNext(object sender, RoutedEventArgs e)
        {
            LoadingBlock.Visibility = Visibility.Visible;
            var client = new HttpClient
            {
                DefaultRequestHeaders =
                                     {
                                         {"X-Result-Page", (CurrentPage + 1).ToString(new NumberFormatInfo())},
                                         {"X-Result-Time", DateTime.Now.ToString("o")},
                                         {"X-Result-Per", "20"},
                                         {"Accept", "application/json"}
                                     }
            };

            var response =
                JsonConvert.DeserializeObjectAsync<WebResponse<Cloud[]>>(
                    await client.GetStringAsync(Endpoints.ExploreRecent));

            foreach (var basic in from Cloud cloud in response.Result.Result select new ItemBasic(cloud) { Margin = new Thickness(10, 10, 10, 10) })
                View.Children.Add(basic);

            LoadingBlock.Visibility = Visibility.Hidden;
            ++CurrentPage;
        }

        private void CheckScrollState(object sender, ScrollChangedEventArgs e)
        {
            var scroll = (ScrollViewer)sender;
            var vo = scroll.VerticalOffset;
            var maxvo = scroll.ExtentHeight - scroll.ViewportHeight;

            if (vo.Equals(maxvo))
            {
                LoadNext(this, null);
            }
        }
    }
}
