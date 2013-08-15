using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CloudsdaleWin7.MVVM
{
    class MessageProcessor : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var SlashIndices = value.ToString().IndexOfAny(new char['/']);

            return value.ToString().Split('\n').Select(line => line.Trim()).ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
        public static List<string> ItalicsSplit(string message)
        {
            var splitItems = new List<string>();
            if (string.IsNullOrEmpty(message)) return splitItems;
            var nextPos = 0;
            var cursorPos = 0;
            while (nextPos > -1)
            {
                nextPos = message.IndexOf('/', cursorPos);
                if (nextPos != -1 && (message.Substring(cursorPos - 1)) == " ")
                {
                    splitItems.Add(message.Substring(cursorPos, nextPos - cursorPos));
                    cursorPos = nextPos + 1;
                }
                splitItems.Add(message.Substring(cursorPos, message.Length - cursorPos));

            }
            return splitItems;
        }
    }
}
