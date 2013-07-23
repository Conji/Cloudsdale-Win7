using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Cloudsdale_Win7.Assets;

namespace Cloudsdale_Win7.MVVM
{
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "online":
                    return Cloudsdale_Source.OnlineStatus;
                case "offline":
                    return Cloudsdale_Source.OfflineStatus;
                case "busy":
                    return Cloudsdale_Source.BusyStatus;
                case "away":
                    return Cloudsdale_Source.AwayStatus;
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
