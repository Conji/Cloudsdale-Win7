using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.MVVM
{
    class SlashMe : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
    class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rtb = (RichTextBox) value;
            var message = rtb.DataContext;
            var newDoc = new FlowDocument();
            var blocks = message.ToString().Split('/');
            return blocks;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
