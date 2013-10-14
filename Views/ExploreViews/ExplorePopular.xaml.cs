using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows;
using CloudsdaleWin7.Views.ExploreViews.ItemViews;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.ExploreViews
{
    /// <summary>
    /// Interaction logic for ExplorePopular.xaml
    /// </summary>
    public partial class ExplorePopular
    {
        public ExplorePopular()
        {
            InitializeComponent();
            FetchPopularList();
            CurrentPage = 1;
        }

        private int CurrentPage { get; set; }

        private void FetchPopularList()
        {
            LoadNext(this, null);
        }

        private async void LoadNext(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient
                             {
                                 DefaultRequestHeaders =
                                     {
                                         {"X-Result-Page", (CurrentPage + 1).ToString(new NumberFormatInfo())},
                                         {"X-Result-Time", DateTime.Now.ToString(new DateTimeFormatInfo())},
                                         {"X-Result-Per", "20"},
                                         {"Accept", "application/json"}
                                     }
                             };

            var response =
                JsonConvert.DeserializeObjectAsync<WebResponse<Cloud[]>>(
                    await client.GetStringAsync(Endpoints.ExplorePopular));

            foreach (var basic in from Cloud cloud in response.Result.Result select new ItemBasic(cloud) { Margin = new Thickness(10, 10, 10, 10) })
                View.Children.Add(basic);

            ++CurrentPage;
        }
    }
}