using System.Windows;
using CloudsdaleWin7.lib.Controllers;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public readonly ConnectionController ConnectionController = new ConnectionController();

        public static ConnectionController Connection
        {
            get { return ((App)Current).ConnectionController; }
        }
    }
}
