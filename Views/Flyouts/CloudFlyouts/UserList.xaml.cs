using System.Collections.Generic;
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
        }

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

                    foreach (var user in Controller.AllUsers.Where(c => c.Name.ToLower().StartsWith(SearchBox.Text.ToLower())))
                    {
                        SearchList.Add(await App.Connection.ModelController.GetUserAsync(user.Id));
                    }

                    //SearchList.AddRange(Controller.AllModerators.Where(u => u.Name.ToLower().StartsWith(SearchBox.Text.ToLower().Trim()) && !SearchList.Contains(u)));
                    //SearchList.AddRange(Controller.AllUsers.Where(u => u.Name.ToLower().StartsWith(SearchBox.Text.ToLower().Trim()) && !SearchList.Contains(u)));
                    break;
            }

        }

        private async void FlyoutUser(object sender, SelectionChangedEventArgs e)
        {
            var user = (User) ((ListView) sender).SelectedItem;
            user.Name = user.Name.Replace("(banned)", "");
            await user.Validate();
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
