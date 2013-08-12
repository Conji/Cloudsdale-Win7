using System;
using System.Data;
using System.Globalization;
using System.Windows.Data;
using CloudsdaleWin7.lib.CloudsdaleLib;

namespace CloudsdaleWin7.MVVM
{
    class AkaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object property, CultureInfo culture)
        {
            var newvalue = value.ToString().MultiReplace("[", "]", "\"", "");
            return newvalue;
        }
        public object ConvertBack(object value, Type targetType, object property, CultureInfo culture)
        {
            throw new SyntaxErrorException();
        }
    }
}
