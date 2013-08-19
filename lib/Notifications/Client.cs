using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace CloudsdaleWin7.lib.Notifications
{
    class Client
    {
        private static string _loginMessage = MainWindow.WebMessage;
        private static Timer _timer = new Timer();
        private static MainWindow Main = new MainWindow();

        public static void Notify()
        {
            if (_loginMessage != null)
            {
                Main.MessageNotification.Visibility = Visibility.Visible;
                Main.Message.Text = _loginMessage;
                Fade();
            }
        }
        public static void Notify(string message)
        {
            Main.MessageNotification.Visibility = Visibility.Visible;
            Main.Message.Text = message;
            Fade();
        }
        private static void Fade()
        {
            _timer.Elapsed += OnTick;
            _timer.Interval = 100;
            _timer.Start();
        }
        private static void OnTick(object sender, EventArgs e)
        {
            
            if (Dispatcher.CurrentDispatcher.CheckAccess() != true)
            {
                Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
                {
                    if (Main.MessageNotification.Opacity >
                        0)
                    {
                        Main.MessageNotification.Opacity -=
                            10;
                    }
                    else
                    {
                        Main.MessageNotification.Visibility
                            = Visibility.Hidden;
                        Main.MessageNotification.Opacity =
                            100;
                        _timer.Stop();
                    }
                }));
            }
        }
    }
}
