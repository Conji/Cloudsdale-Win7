using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CloudsdaleWin7.lib;

namespace CloudsdaleWin7.MVVM
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
                    return new SolidColorBrush(CloudsdaleSource.FounderTag);
                case "developer":
                    return new SolidColorBrush(CloudsdaleSource.DevTag);
                case "admin":
                    return new SolidColorBrush(CloudsdaleSource.AdminTag);
                case "donor":
                    return new SolidColorBrush(CloudsdaleSource.DonatorTag);
                case "legacy":
                    return new SolidColorBrush(CloudsdaleSource.LegacyTag);
                case "associate":
                    return new SolidColorBrush(CloudsdaleSource.AssociateTag);
                case "verified":
                    return new SolidColorBrush(CloudsdaleSource.VerifiedTag);
                default:
                    return new SolidColorBrush(CloudsdaleSource.PrimaryBackground);
            }
        }
        public Object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HasRank : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "developer" || value.ToString() == "founder") return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
