using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Cloudsdale_Win7.Cloudsdale;

namespace Cloudsdale_Win7.MVVM
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
