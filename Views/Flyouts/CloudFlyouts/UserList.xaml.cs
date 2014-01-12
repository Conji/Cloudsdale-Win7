using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.Views.Flyouts.CloudFlyouts
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList
    {
        public static UserList Instance;
        private static readonly ObservableCollection<User> SearchList = new ObservableCollection<User>(); 
        private static CloudController Controller { get; set; }

        public UserList(CloudController cloud)
        {
            InitializeComponent();
            Instance = this;
            Controller = cloud;
            OwnerList.Items.Add(cloud.Owner);
            ModeratorList.ItemsSource = cloud.OnlineModerators;
            OnlineUserList.ItemsSource = cloud.OnlineUsers;
            SearchResults.ItemsSource = SearchList;
            foreach (var user in Controller.OnlineUsers)
            {
                SearchList.Add(App.Connection.ModelController.GetUserAsync(user.Id).Result);
            }
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
        private async void UpdateSearch(object sender, TextChangedEventArgs e)
        {
            switch(SearchBox.Text.ToLower().Trim())
            {
                case "":
                    SearchScroll.Visibility = Visibility.Collapsed;
                    UserScroll.Visibility = Visibility.Visible;
                    SearchList.Clear();
                    break;
                default:
                    SearchScroll.Visibility = Visibility.Visible;
                    UserScroll.Visibility = Visibility.Collapsed;
                    SearchList.Clear();

                    //TODO: show the search results
                    foreach (var user in Controller.OnlineModerators.Where(u => u.Name.ToLower().StartsWith(SearchBox.Text.ToLower().Trim()) && !SearchList.Contains(u)))
                    {
                        SearchList.Add(user);
                    }

                    foreach(var user in Controller.OnlineUsers.Where(u => u.Name.ToLower().StartsWith(SearchBox.Text.ToLower().Trim()) && !SearchList.Contains(u)))
                    {
                        SearchList.Add(user);
                    }


                    break;
            }
        }

        private void FlyoutUser(object sender, SelectionChangedEventArgs e)
        {
            var user = (User) ((ListView) sender).SelectedItem;
            user.Name = user.Name.Replace("(banned)", "");
            user.ShowFlyout();
        }

        private async void ReloadUsers(object sender, RoutedEventArgs e)
        {
            await Controller.LoadUsers();
            await Controller.LoadBans();
            OnlineUserList.ItemsSource = Controller.OnlineUsers;
            ModeratorList.ItemsSource = Controller.OnlineModerators;
        }

        private void ClosePanel(object sender, RoutedEventArgs e)
        {
            Main.Instance.HideFlyoutMenu();
        }
    }
}
