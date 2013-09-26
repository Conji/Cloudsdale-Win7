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
            var cloud = (Cloud) value;
            if (App.Connection.MessageController[cloud].UnreadMessages == 0) return "";
            return App.Connection.MessageController[cloud].UnreadMessages;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Could not convert back!");
        }
    }
}
