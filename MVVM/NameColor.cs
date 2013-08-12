using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using CloudsdaleWin7.lib;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.MVVM {
    public class NameColor : IValueConverter {
        #region Implementation of IValueConverter

        private static JObject Cloud;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cloud = ((DependencyJToken) parameter).Token;
            var cloudId = cloud["id"].ToString();
            var user = (JToken) value;
            var ownerId = (string) cloud["owner_id"];
            var moderatorIds = cloud["moderator_ids"].Select(id => (string) id).ToList();
            var userId = (string) user["id"];
            return new SolidColorBrush(
                userId == ownerId
                    ? Color.FromRgb(0x66, 0x00, 0xcc)
                    : moderatorIds.Contains(userId)
                          ? Color.FromRgb(0x00, 0xa5, 0xff)
                          : Color.FromRgb(0x57, 0x57, 0x57));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
