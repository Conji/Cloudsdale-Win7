using System;
using System.Collections.Generic;
using System.Net.Http;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.Views.ExploreViews
{
    /// <summary>
    /// Interaction logic for ExplorePopular.xaml
    /// </summary>
    public partial class ExplorePopular
    {

        private readonly Dictionary<string, Cloud> _popularList = new Dictionary<string, Cloud>(); 

        public ExplorePopular()
        {
            InitializeComponent();
            FetchPopularList();
        }
        private void FetchPopularList()
        {
            var client = new HttpClient().AcceptsJson();
            client.DefaultRequestHeaders.Add("X-Page", "1");
            var jsonObject = (JObject.Parse(client.GetStringAsync(Endpoints.ExplorePopular).Result))["result"];
            foreach (JObject o in jsonObject)
            {
                var cloud = o.ToObject<Cloud>();
                _popularList.Add(cloud.Id, cloud);
            }
        }
    }
}
