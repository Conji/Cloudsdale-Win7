using System.Windows.Controls;

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
        }
    }
}
