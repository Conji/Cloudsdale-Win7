using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Cloudsdale_Win7.MVVM {
    public class TagColorConverter : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var color = Colors.Transparent;
            switch (value.ToString()) {
                case "developer":
                    color = Assets.Assets.DevTag;
                    break;
                case "verified":
                    color = Assets.Assets.VerifiedTag;
                    break;
                case "founder":
                    color = Assets.Assets.FounderTag;
                    break;
                case "donor":
                    color = Assets.Assets.DonatorTag;
                    break;
                case "admin":
                    color = Assets.Assets.AdminTag;
                    break;
                case "legacy":
                    color = Assets.Assets.LegacyTag;
                    break;
                case "associate":
                    color = Assets.Assets.AssociateTag;
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
