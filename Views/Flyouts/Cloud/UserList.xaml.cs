using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.Flyouts.Cloud
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList
    {
        public static UserList Instance;
        private readonly static ObservableCollection<User> SearchList = new ObservableCollection<User>(); 
        private static CloudController Controller { get; set; }

        public UserList(CloudController cloud)
        {
            InitializeComponent();
            Instance = this;
            Controller = cloud;
            OwnerList.Items.Add(App.Connection.ModelController.GetUser(cloud.Cloud.OwnerId));
            ModeratorList.ItemsSource = cloud.OnlineModerators;
            OnlineUserList.ItemsSource = cloud.OnlineUsers;
            SearchResults.ItemsSource = SearchList;
            App.Connection.MessageController[cloud.Cloud].EnsureLoaded();
            Console.WriteLine(GetOwner().Id);
        }

        /// <summary>
        /// Updates the search box object according to text.
        /// It will organize itself by:
        /// - online users that match the criteria
        /// - online users that contain the text
        /// - offline users that match the criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSearch(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                SearchScroll.Visibility = Visibility.Collapsed;
                UserScroll.Visibility = Visibility.Visible;
                SearchList.Clear();
            }
            else
            {
                SearchScroll.Visibility = Visibility.Visible;
                UserScroll.Visibility = Visibility.Collapsed;
                SearchList.Clear();
                // Collects the online users list first.
                foreach (var user in Controller.OnlineUsers)
                {
                    if (user.Name == null) return;
                    if (user.Name.ToLower().StartsWith(SearchBox.Text.ToLower()))
                    {
                        SearchList.Add(user);
                    }
                }
                //Collects the offline users list next.
                foreach (var user in Controller.AllUsers)
                {
                    if (user.Name == null) return;
                    if (SearchBox.Text.Trim() == "[all]")
                    {
                        
                    }
                    if (user.Name.ToLower().StartsWith(SearchBox.Text.ToLower()))
                    {
                        //SearchList.Add(user);
                        if (SearchList.Contains(user)) return;
                        SearchList.Add(user);
                    }
                }
            }
        }
        private static User GetOwner()
        {
            var client = new HttpClient().AcceptsJson();
            var response = JsonConvert.DeserializeObject<User>(client.GetStringAsync(Endpoints.UserJson.Replace("[:id]", Controller.Cloud.OwnerId)).Result);
            return response;
        }

        private void FlyoutUser(object sender, SelectionChangedEventArgs e)
        {
            var user = (User) ((ListView) sender).SelectedItem;
            user.ShowFlyout(Controller.Cloud);
        }

    }
}
