using System;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.lib.Controllers;

namespace CloudsdaleWin7.Views.Flyouts.Cloud
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList
    {
        public static UserList Instance;
        private static CloudController Controller { get; set; }

        public UserList(CloudController cloud)
        {
            InitializeComponent();
            Instance = this;
            Controller = cloud;
            ModeratorList.ItemsSource = cloud.OnlineModerators;
            OnlineUserList.ItemsSource = cloud.OnlineUsers;
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
            }
            else
            {
                SearchUI.Visibility = Visibility.Visible;
                OnlineUI.Visibility = Visibility.Collapsed;
                SearchResults.Items.Clear();
                foreach (var user in Controller.AllUsers)
                {
                    if (user.Name.StartsWith(SearchBox.Text))
                    {
                        SearchResults.Items.Add(user);
                    }
                }
            }
            
        }
    }
}
