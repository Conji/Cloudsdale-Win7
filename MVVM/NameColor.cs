using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.MVVM {
    public class NameColor : IValueConverter {
        #region Implementation of IValueConverter

        private readonly static CloudsdaleApp App = MainWindow.MainApp;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = (User)value;
            var controller = App.MessageController.CurrentCloud;
            var cloud = controller.Cloud;

            var color = cloud.OwnerId == user.Id
                            ? Color.FromArgb(0xFF, 0x80, 0x00, 0xFF)
                            : cloud.ModeratorIds.Contains(user.Id)
                                  ? Color.FromArgb(0xFF, 0x33, 0x66, 0xFF)
                                  : Color.FromArgb(0xFF, 0x5A, 0x5A, 0x5A);
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
