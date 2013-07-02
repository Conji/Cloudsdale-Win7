using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Cloudsdale.actions.MessageController
{
    class ChatColorController : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(value != null && value.ToString().StartsWith(">") && !value.ToString().StartsWith(">.>") ? Colors.LimeGreen : Colors.Black);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("An error occured when trying to convert the text back.");
        }
    }
}