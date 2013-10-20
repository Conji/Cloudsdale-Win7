using System;
using System.Globalization;
using System.Windows.Data;

namespace CloudsdaleWin7.MVVM
{
    class StringShorten : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value.ToString().Length > 15 ? value.ToString().Remove(15) + "...": value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
