using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CloudsdaleWin7.lib;

namespace CloudsdaleWin7.MVVM
{
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
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
