using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Misc;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private readonly string Name = App.Connection.SessionController.CurrentSession.Name;
        public static Home Instance;
        public Home()
        {
            InitializeComponent();
            Instance = this;
            Welcome.Text = WelcomeMessage(Name);
            AviImage.Source = new BitmapImage(App.Connection.SessionController.CurrentSession.Avatar.Normal);
            Animate();
        }
        private static string WelcomeMessage(string name)
        {
            var r = new Random();
            String message;
            switch(r.Next(0,5))
            {
                case 0:
                    message = "Hi, [:name]!";
                    break;
                case 1:
                    message = "Welcome, [:name]!";
                    break;
                case 2:
                    message = "Welcome back, [:name].";
                    break;
                case 3:
                    message = "Hi there, [:name]!";
                    break;
                case 4:
                    if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12) message = "Good morning, [:name]~";
                    else if (DateTime.Now.Hour < 6) message = "It's a lovely night, [:name].";
                    else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 20) message = "Good afternoon [:name].";
                    else message = "Howdy, [:name]~";
                    break;
                default:
                    message = "Hello, [:name].";
                    break;
            }
            return message.Replace("[:name]", name);
        }
        private void Animate()
        {
            #region Welcome Message
            var board = new Storyboard();
            var animation = new DoubleAnimation(0, 100, new Duration(new TimeSpan(2000000000)));
            board.Children.Add(animation);
            Storyboard.SetTargetName(animation, Welcome.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
            animation.EasingFunction = new ExponentialEase();
            board.Begin(this);

            #region FancyLines

            //The line underneath the name first
            Line1.BeginAnimation(WidthProperty,
                new DoubleAnimation(0, MainWindow.Instance.Width - 400, new Duration(new TimeSpan(0, 0, 1)),
                    FillBehavior.HoldEnd));
            Avi.BeginAnimation(MarginProperty,
                new ThicknessAnimation(new Thickness(0, -1000, 100, 1010), new Thickness(0, 100, 100, 200),
                    new Duration(new TimeSpan(0, 0, 1)))
                {
                    EasingFunction = new BounceEase {Bounces = 3, Bounciness = 4}
                });

            #endregion

            #endregion
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Line1.Width = MainWindow.Instance.Width - 400;
        }

        private void DirectAbout(object sender, RoutedEventArgs e)
        {
            Main.Instance.Frame.Navigate(new About());
        }
    }
}
