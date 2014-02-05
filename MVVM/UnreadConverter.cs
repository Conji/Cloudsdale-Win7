using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.MVVM
{
    class UnreadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = (int) value;
            if (i >= 1000) return "999+";
            if (i > 0) return value.ToString();
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Could not convert back!");
        }
    }
}
