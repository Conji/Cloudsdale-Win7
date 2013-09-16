using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Models;

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
            OwnerList.Items.Clear();
            OwnerList.Items.Add(new User(cloud.Cloud.OwnerId));
            ModeratorList.ItemsSource = cloud.OnlineModerators;
            OnlineUserList.ItemsSource = cloud.OnlineUsers;
            SearchResults.ItemsSource = SearchList;
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
                SearchUI.Visibility = Visibility.Collapsed;
                OnlineUI.Visibility = Visibility.Visible;
                SearchList.Clear();
            }
            else
            {
                SearchUI.Visibility = Visibility.Visible;
                OnlineUI.Visibility = Visibility.Collapsed;
                SearchList.Clear();
                foreach (var user in Controller.OnlineUsers)
                {
                    if (user.Name.StartsWith(SearchBox.Text))
                    {
                        SearchList.Add(user);
                    }
                }
            }
            
        }
    }
}
