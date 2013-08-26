using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CloudsdaleWin7.lib.CloudsdaleLib;

namespace CloudsdaleWin7.MVVM
{
    class UnreadVisibility : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cloudId = (string) value;
            return MessageSource.GetSource(cloudId).UnreadMessages == 0 ? Visibility.Hidden : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
