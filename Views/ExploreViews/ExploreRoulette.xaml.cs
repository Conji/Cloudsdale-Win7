using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.ExploreViews
{
    /// <summary>
    /// Interaction logic for ExploreRoulette.xaml
    /// </summary>
    public partial class ExploreRoulette
    {

        private readonly ObservableCollection<Cloud> _gathered = new ObservableCollection<Cloud>(); 

        /// <summary>
        /// Steps to roulette.
        /// 1: Gather 6 clouds out of 100
        /// 2: Adds them to the wheel
        /// 3: Skips over a rand of 4-20 clouds and chooses.
        /// </summary>
        public ExploreRoulette()
        {
            InitializeComponent();
            Begin();
        }

        private void Begin()
        {
            GatherClouds();
        }

        public async void GatherClouds()
        {
            CurrentStatus.Text = "Gathering clouds...";
            var client = new HttpClient
            {
                DefaultRequestHeaders =
                                     {
                                         {"X-Result-Time", DateTime.Now.ToString("o")},
                                         {"X-Result-Per", "100"},
                                         {"Accept", "application/json"}
                                     }
            };

            var response =
                JsonConvert.DeserializeObjectAsync<WebResponse<Cloud[]>>(
                    await client.GetStringAsync(Endpoints.ExplorePopular));

            foreach(var cloud in response.Result.Result)
            {
                cloud.ForceValidate();
                _gathered.Add(cloud);
            }
            CurrentStatus.Text = "Finished gathering clouds.";
        }

        

    }
}
