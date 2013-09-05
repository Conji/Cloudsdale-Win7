using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Flyouts.Cloud
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Page
    {
        public static UserList Instance;
        private static lib.Models.Cloud _Cloud;
        private static readonly CloudController _controller = new CloudController(_Cloud);

        public UserList(lib.Models.Cloud cloud)
        {
            InitializeComponent();
            Instance = this;
            _Cloud = cloud;
            var test = new CloudController(cloud);
            OwnerList.Items.Add(new User(test.Cloud.OwnerId));
            Console.WriteLine(test.Cloud.OwnerId);
        }

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
                foreach (var user in _controller.AllUsers)
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
