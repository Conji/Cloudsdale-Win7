using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Cloudsdale_Win7.MVVM
{
    internal class ItalicsConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newStyle = new FontStyle();
            foreach (var slash in value.ToString())
            {
                var _style = new FontStyle();
                if(slash == '/')
                {
                    if (_style == FontStyles.Italic)
                    {
                        newStyle = FontStyles.Normal;
                    }else
                    {
                        newStyle = FontStyles.Italic;
                    }
                }
            }
            return newStyle;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Could not convert!");
        }
    }
}
