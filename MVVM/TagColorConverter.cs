using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CloudsdaleWin7.MVVM {
    public class TagColorConverter : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var color = Colors.Transparent;
            switch (value.ToString()) {
                case "staff":
                    color = lib.CloudsdaleSource.DevTag;
                    break;
                case "developer":
                    color = lib.CloudsdaleSource.DevTag;
                    break;
                case "verified":
                    color = lib.CloudsdaleSource.VerifiedTag;
                    break;
                case "founder":
                    color = lib.CloudsdaleSource.FounderTag;
                    break;
                case "donor":
                    color = lib.CloudsdaleSource.DonatorTag;
                    break;
                case "admin":
                    color = lib.CloudsdaleSource.AdminTag;
                    break;
                case "legacy":
                    color = lib.CloudsdaleSource.LegacyTag;
                    break;
                case "associate":
                    color = lib.CloudsdaleSource.AssociateTag;
                    break;
                case "normal":
                    color = lib.CloudsdaleSource.PrimaryBackground;
                    break;
                default:
                    color = Colors.Lavender;
                    break;
            }
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
