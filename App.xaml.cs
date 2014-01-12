using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.CloudsdaleLib.Misc.Screenshot;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Faye;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public readonly ConnectionController ConnectionController = new ConnectionController();
        public static Settings Settings = new Settings();

        public static ConnectionController Connection
        {
            get { return ((App)Current).ConnectionController; }
        }

        public static void Close()
        {
            ViewCapture.Instance.Close();
            if (FayeConnector.Socket != null && FayeConnector.Connected) FayeConnector.Socket.Close();
            Settings.Save();
        }
    }
}
