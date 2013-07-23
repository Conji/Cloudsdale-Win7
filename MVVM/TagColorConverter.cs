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
                    color = Assets.Cloudsdale_Source.DevTag;
                    break;
                case "verified":
                    color = Assets.Cloudsdale_Source.VerifiedTag;
                    break;
                case "founder":
                    color = Assets.Cloudsdale_Source.FounderTag;
                    break;
                case "donor":
                    color = Assets.Cloudsdale_Source.DonatorTag;
                    break;
                case "admin":
                    color = Assets.Cloudsdale_Source.AdminTag;
                    break;
                case "legacy":
                    color = Assets.Cloudsdale_Source.LegacyTag;
                    break;
                case "associate":
                    color = Assets.Cloudsdale_Source.AssociateTag;
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
