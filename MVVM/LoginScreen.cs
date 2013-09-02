using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CloudsdaleWin7.MVVM
{
    class LoginScreen : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 20) color = Color.FromRgb(99, 160, 208);
            else color = Color.FromRgb(100, 70, 130);
            return color;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
}
