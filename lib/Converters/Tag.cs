using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CloudsdaleWin7.lib.Converters
{
    public class TagVisibility : IValueConverter
    {
        public Object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = (string) value;
            return role == "normal" || string.IsNullOrWhiteSpace(role) ? Visibility.Collapsed : Visibility.Visible;
        }
        public Object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Could not convert back!");
        }
    }
    public class TagText : IValueConverter
    {
        public Object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = (string) value;
            return role == "developer" ? "dev" : role;
        }
        public Object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TagColor : IValueConverter
    {
        public Object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = (string) value;
            switch (role)
            {
                case "founder":
                    return CloudsdaleSource.FounderTag;
                case "developer":
                    return CloudsdaleSource.DevTag;
                case "admin":
                    return CloudsdaleSource.AdminTag;
                case "donor":
                    return CloudsdaleSource.DonatorTag;
                case "legacy":
                    return CloudsdaleSource.LegacyTag;
                case "verified":
                    return CloudsdaleSource.VerifiedTag;
                default:
                    return CloudsdaleSource.LegacyTag;
            }
        }
        public Object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
