using System;
using System.Globalization;
using System.Windows.Data;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.MVVM
{
    class SlashMe : IValueConverter 
    {
        public object Convert(object value, Type targetType, object paramter, CultureInfo culture)
        {
            var message = (Message) value;
            var content = message.Content;
            if (content.StartsWith("/me "))
            {
                content = content.Replace("/me", message.Author.Name);
            }
            return content;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
