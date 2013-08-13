using System;
using System.Globalization;
using System.Windows.Data;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.MVVM {
    class TimeOfDayToString : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var jToken = value as JToken;
            return (jToken != null ? (jToken.ToObject<DateTime>()) : ((DateTime)value)).TimeOfDay.ToString(@"hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
