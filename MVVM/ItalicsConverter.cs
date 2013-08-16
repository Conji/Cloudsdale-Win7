using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace CloudsdaleWin7.MVVM
{
    internal class ItalicsConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Could not convert!");
        }
    }
}
