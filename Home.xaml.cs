using System.Windows.Controls;
using Cloudsdale_Win7.Models;

namespace Cloudsdale_Win7 {
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public static Home Instance;
        public Home() {
            InitializeComponent();
            RootGrid.DataContext = MainWindow.User;
            Instance = this;
            MainWindow.Instance.CloudList.Width = 200;
            JoinDate.Text += UserModel.MemberSince(MainWindow.User["user"]["id"].ToString()).Split(' ')[0];
        }
    }
}
