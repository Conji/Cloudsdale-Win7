using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
        private static ObservableCollection<User> _searchList = new ObservableCollection<User>(); 
        private static CloudController Controller { get; set; }

        public UserList(CloudController cloud)
        {
            InitializeComponent();
            Instance = this;
            Controller = cloud;
            App.Connection.MessageController[cloud.Cloud].LoadUsers();
            App.Connection.MessageController[cloud.Cloud].LoadBans();
            OwnerList.Items.Add(cloud.Owner);
            ModeratorList.ItemsSource = cloud.OnlineModerators;
            OnlineUserList.ItemsSource = cloud.OnlineUsers;
            SearchResults.ItemsSource = _searchList;
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
            if (SearchBox.Text == "")
            {
                SearchScroll.Visibility = Visibility.Collapsed;
                UserScroll.Visibility = Visibility.Visible;
                _searchList.Clear();
            }
            else
            {
                SearchScroll.Visibility = Visibility.Visible;
                UserScroll.Visibility = Visibility.Collapsed;
                _searchList.Clear();

                if (SearchBox.Text == "!")
                {
                    var cacheList = new ObservableCollection<User>();
                    foreach (var response in from userId in Controller.BansByUser.Keys let client = new HttpClient().AcceptsJson() select JsonConvert.DeserializeObjectAsync<WebResponse<User>>(
                        client.GetStringAsync(Endpoints.User.Replace("[:id]", userId)).Result))
                    {
                        cacheList.Add(response.Result.Result);
                    }
                    _searchList = cacheList;
                    return;
                }

                if (SearchBox.Text != "?")
                {
                    // Collects the online users list first.
                    foreach (var user in Controller.OnlineUsers.Where(user => user.Name == null))
                    {
                        await user.ForceValidate();
                    }
                    foreach (var user in Controller.OnlineUsers.Where(user => user.Name != null && user.Name.ToLower().StartsWith(SearchBox.Text.ToLower())))
                    {
                        if (App.Connection.ModelController.Users.ContainsValue(user))
                        {
                            _searchList.Add(App.Connection.ModelController.Users[user.Id]);
                            return;
                        }

                        if (Controller.BansByUser.ContainsKey(user.Id))
                        {
                            user.Name += "(banned)";
                        }

                        if (_searchList.Contains(user)) return;
                        _searchList.Add(user);
                       
                    }

                    //Collects the offline users list next.
                    foreach (var user in Controller.AllUsers.Where(user => user.Name == null))
                    {
                        await user.ForceValidate();
                    }
                    foreach (var user in Controller.AllUsers.Where(user => user.Name != null && user.Name.ToLower().StartsWith(SearchBox.Text.ToLower())))
                    {
                        if (_searchList.Contains(user)) return;
                        if (Controller.BansByUser.ContainsKey(user.Id))
                        {
                            user.Name += "(banned)";
                        }

                        _searchList.Add(user);
                    }
                    return;
                }

                //Fetches all users
                _searchList.Clear();
                
                foreach (var id in Controller.Cloud.UserIds)
                {
                   try
                   {
                       var client = new HttpClient().AcceptsJson();
                       var response = await client.GetStringAsync(Endpoints.User.Replace("[:id]", id));


                       var user = await JsonConvert.DeserializeObjectAsync<WebResponse<User>>(response);
                       if (_searchList.Contains(user.Result)) return;
                       App.Connection.ModelController.UpdateDataAsync(user.Result);
                       
                       if (Controller.BansByUser.ContainsKey(user.Result.Id))
                       {
                           user.Result.Name += "(banned)";
                       }

                       _searchList.Add(user.Result);
                   }
                   catch
                   {
                       var user = new User(id);
                       user.ForceValidate();
                       _searchList.Add(user);
                   }
                }
            }
        }

        private void FlyoutUser(object sender, SelectionChangedEventArgs e)
        {
            var user = (User) ((ListView) sender).SelectedItem;
            user.Name = user.Name.Replace("(banned)", "");
            user.ShowFlyout(Controller.Cloud);
        }

        private async void ReloadUsers(object sender, RoutedEventArgs e)
        {
            await Controller.LoadUsers();
            await Controller.LoadBans();
            OnlineUserList.ItemsSource = Controller.OnlineUsers;
            ModeratorList.ItemsSource = Controller.OnlineModerators;
        }

    }
}
