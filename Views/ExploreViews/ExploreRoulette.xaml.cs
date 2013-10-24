using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Timers;
using System.Windows;
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
        private readonly ObservableCollection<Cloud> _assigned = new ObservableCollection<Cloud>();
        private Cloud FinalCloud { get; set; }

        /// <summary>
        /// Steps to roulette.
        /// 1: Gather 6 clouds out of 100
        /// 2: Adds them to the wheel
        /// 3: Skips over a rand of 4-20 clouds and chooses.
        /// </summary>
        public ExploreRoulette()
        {
            InitializeComponent();
        }

        private void Begin()
        {
            GatherClouds();
        }

        public async void GatherClouds()
        {
            FinalizedUI.Visibility = Visibility.Hidden;
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


            CurrentStatus.Text = "Assigning clouds...";
            var newMax = new Random().Next(4, 20);
            while (_assigned.Count < newMax)
            {
                _assigned.Add(_gathered[new Random().Next(0, 99)]);
            }
            CurrentStatus.Text = "Finished assigning clouds.";


            CurrentStatus.Text = "Roulette has gathered " + newMax + " clouds for you. Preparing to choose...";
            var clock = new Timer
                            {
                                Interval = 1000,
                            };
            clock.Start();
            clock.Elapsed += delegate
                                 {
                                     var sec = 0;
                                     if (sec < 5)
                                     {
                                         ++sec;
                                         return;
                                     }
                                     clock.Stop();
                                 };
            var finalInt = new Random().Next(0, newMax - 1);
            FinalCloud = _assigned[finalInt];
            Join(this, null);
        }

        private async void Join(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient().AcceptsJson();
            var response =
                JsonConvert.DeserializeObjectAsync<WebResponse<Cloud>>(
                    await client.GetStringAsync(Endpoints.Cloud.Replace("[:id]", FinalCloud.Id)));
            BrowserHelper.JoinCloud(response.Result.Result);
        }

        private void BeginRoulette(object sender, RoutedEventArgs e)
        {
            Begin();
        }
        
    }
}
