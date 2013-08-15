using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using CloudsdaleWin7.lib;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.MVVM
{
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = value.ToString();
            switch (obj)
            {
                case "online":
                    return CloudsdaleSource.OnlineStatus;
                case "offline":
                    return CloudsdaleSource.OfflineStatus;
                case "busy":
                    return CloudsdaleSource.BusyStatus;
                case "away":
                    return CloudsdaleSource.AwayStatus;
                default:
                    return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
