using System;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using CloudsdaleWin7.Views.ExploreViews.ItemViews;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

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

        private void LoadNext(object sender, RoutedEventArgs e)
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

            var jsonObject = (JObject.Parse(client.GetStringAsync(Endpoints.ExplorePopular).Result))["result"];

            foreach (JObject o in jsonObject)
            {
                var cloud = o.ToObject<Cloud>();
                var basic = new ItemBasic(cloud);
                basic.Margin = new Thickness(30, 30, 30, 30);

                View.Children.Add(basic);
            }
            CurrentPage += 1;
        }
    }
}