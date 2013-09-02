using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.MVVM
{
    class IsUnread :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var data = (TextBlock) value;
            //var currentCloud = (JToken) MainWindow.Instance.CloudList.SelectedItem;

            //if (currentCloud["id"] != data.DataContext)
            //{
            //    return MessageSource.GetSource(data.DataContext.ToString()).UnreadMessages == 0
            //               ? ""
            //               : MessageSource.GetSource(data.DataContext.ToString()).UnreadMessages.ToString();
            //}
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new ValueUnavailableException();
        }
    }
}
