using System;
using System.Globalization;
using System.Windows.Data;

namespace CloudsdaleWin7.MVVM
{
    class StringShorten : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value.ToString().Length > 20 ? value.ToString().Remove(20) + "...": value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
