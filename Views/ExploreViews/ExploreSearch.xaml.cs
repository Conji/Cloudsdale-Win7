using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CloudsdaleWin7.Views.ExploreViews.ItemViews;

namespace CloudsdaleWin7.Views.ExploreViews
{

    /// <summary>
    /// Interaction logic for ExploreSearch.xaml
    /// </summary>
    public partial class ExploreSearch : Page
    {
        public ExploreSearch(IEnumerable<ItemBasic> items)
        {
            InitializeComponent();
            foreach (var item in items)
            {
                item.Margin = new Thickness(5,5,5,5);
                CloudHost.Children.Add(item);
            }
        }
    }
}
