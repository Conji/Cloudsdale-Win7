using System;
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
        private readonly static ObservableCollection<User> SearchList = new ObservableCollection<User>(); 
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
            App.Connection.MessageController[cloud.Cloud].LoadUsers();
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
                SearchList.Clear();
            }
            else
            {
                SearchScroll.Visibility = Visibility.Visible;
                UserScroll.Visibility = Visibility.Collapsed;
                SearchList.Clear();

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
                            SearchList.Add(App.Connection.ModelController.Users[user.Id]);
                            return;
                        }
                        if (SearchList.Contains(user)) return;
                        SearchList.Add(user);
                       
                    }

                    //Collects the offline users list next.
                    foreach (var user in Controller.AllUsers.Where(user => user.Name == null))
                    {
                        await user.ForceValidate();
                    }
                    foreach (var user in Controller.AllUsers.Where(user => user.Name != null && user.Name.ToLower().StartsWith(SearchBox.Text.ToLower())))
                    {
                        if (SearchList.Contains(user)) return;
                        SearchList.Add(user);
                    }
                    return;
                }

                //Fetches all users
                SearchList.Clear();
                
                foreach (var id in Controller.Cloud.UserIds)
                {
                   try
                   {
                       var client = new HttpClient().AcceptsJson();
                       var response = await client.GetStringAsync(Endpoints.User.Replace("[:id]", id));


                       var user = await JsonConvert.DeserializeObjectAsync<WebResponse<User>>(response);
                       if (SearchList.Contains(user.Result)) return;
                       App.Connection.ModelController.UpdateDataAsync(user.Result);
                       SearchList.Add(user.Result);
                   }catch(Exception ex)
                   {
                       Console.WriteLine(ex.Message);
                       var user = new User(id);
                       user.ForceValidate();
                       SearchList.Add(user);
                   }
                }
            }
        }

        private void FlyoutUser(object sender, SelectionChangedEventArgs e)
        {
            var user = (User) ((ListView) sender).SelectedItem;
            user.ShowFlyout(Controller.Cloud);
        }

        private async void ReloadUsers(object sender, RoutedEventArgs e)
        {
            await Controller.LoadUsers();
            OnlineUserList.ItemsSource = Controller.AllUsers;
        }

    }
}
