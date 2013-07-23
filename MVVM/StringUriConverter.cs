using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cloudsdale_Win7.MVVM {
    [ValueConversion(typeof(string), typeof(Uri))]
    public class StringUriConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new Uri(value.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((Uri) value).AbsoluteUri;
        }
    }
}
