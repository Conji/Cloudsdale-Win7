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
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.CloudsdaleLib;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for Explore.xaml
    /// </summary>
    public partial class Explore : Page
    {
        public Explore()
        {
            InitializeComponent();
        }

        private void LoadPreviousSource()
        {
            switch (App.Settings["selected_source"])
            {
                case "popular":
                    // nav to popular view
                    
                    break;
                case "recent":
                    // nav to recent view
                    break;
                case "top":
                    // nav to top view
                    break;
                case "roulette":
                    // nav to roulette view
                    break;
                default:
                    //nav to popular view
                    break;
            }
        }

        private void ChangeExploreSource(object sender, RoutedEventArgs e)
        {
            foreach (Button button in ViewPanel.Children)
            {
                button.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlue);
            }
            var senderName = ((Button) sender).Name;
            switch (senderName)
            {
                case "CmdPopular":
                    App.Settings.ChangeSetting("selected_source", "popular");
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "CmdRecent":
                    App.Settings.ChangeSetting("selected_source", "recent");
                    CmdRecent.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "CmdTop":
                    App.Settings.ChangeSetting("selected_source", "top");
                    CmdTop.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "CmdRoulette":
                    App.Settings.ChangeSetting("selected_source", "roulette");
                    CmdRoulette.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;

            }
        }
    }
}
