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
                case "staff":
                    color = Win7_Lib.Cloudsdale_Source.DevTag;
                    break;
                case "developer":
                    color = Win7_Lib.Cloudsdale_Source.DevTag;
                    break;
                case "verified":
                    color = Win7_Lib.Cloudsdale_Source.VerifiedTag;
                    break;
                case "founder":
                    color = Win7_Lib.Cloudsdale_Source.FounderTag;
                    break;
                case "donor":
                    color = Win7_Lib.Cloudsdale_Source.DonatorTag;
                    break;
                case "admin":
                    color = Win7_Lib.Cloudsdale_Source.AdminTag;
                    break;
                case "legacy":
                    color = Win7_Lib.Cloudsdale_Source.LegacyTag;
                    break;
                case "associate":
                    color = Win7_Lib.Cloudsdale_Source.AssociateTag;
                    break;
                case "normal":
                    color = Win7_Lib.Cloudsdale_Source.PrimaryBackground;
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
