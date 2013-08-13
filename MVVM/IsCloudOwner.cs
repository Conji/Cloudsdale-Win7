using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CloudsdaleWin7.MVVM
{
    class IsCloudOwner :IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cloudOwner = MainWindow.CurrentCloud["owner_id"].ToString();
            var id = MainWindow.User["user"]["id"].ToString();
            if (cloudOwner != id) return Visibility.Collapsed;
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new ValueUnavailableException();
        }
    }
}
