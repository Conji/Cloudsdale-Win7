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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.ExploreViews.ItemViews
{
    /// <summary>
    /// Interaction logic for ItemBasic.xaml
    /// </summary>
    public partial class ItemBasic : UserControl
    {
        public ItemBasic(Cloud cloud)
        {
            InitializeComponent();
        }

        private void ShowHiddenUI(object sender, MouseEventArgs e)
        {
            var a = new ThicknessAnimation(new Thickness(0,-38,0,39), new Thickness(0,0,0,39), new Duration(new TimeSpan(0,0,1)));
            a.EasingFunction = new ExponentialEase();
            BackUI.BeginAnimation(MarginProperty, a);
        }

        private void HideHiddenUI(object sender, MouseEventArgs e)
        {
            var a = new ThicknessAnimation(new Thickness(0, 0, 0, 39), new Thickness(0, -38, 0, 39),
                                           new Duration(new TimeSpan(0, 0, 1)));
            BackUI.BeginAnimation(MarginProperty, a);
        }
    }
}
