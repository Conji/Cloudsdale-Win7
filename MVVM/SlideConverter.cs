using System;
using System.Globalization;
using System.Windows.Data;

namespace CloudsdaleWin7.MVVM
{
    class SlideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int newWidth;
            if (MainWindow.Instance.MenuPanel.Width.Equals(80))
            {
                newWidth = 0;
            }
            else
            {
                newWidth = 90;
            }
            return newWidth;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
