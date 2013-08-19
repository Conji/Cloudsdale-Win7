using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace CloudsdaleWin7.MVVM
{
    class MessageProcessor : IValueConverter 
    {
        public static readonly Regex ItalicsRegex = new Regex(@"\B\/\b([^\/\n]+)\b\/\B");
        public static readonly Regex RedactedRegex = new Regex(@"\[REDACTED\]", RegexOptions.IgnoreCase);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            #region Italics
            foreach (string word in ItalicsSplit(value.ToString()))
            {
                var content = new TextBlock();

                content.Inlines.Add(new Italic(new Run(word)));
            }

            #endregion

            return value.ToString().Split('\n').Select(line => line.Trim()).ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
        public static List<string> ItalicsSplit(string text)
        {
            List<string> splitItems = new List<string>();
            foreach (Match matches in ItalicsRegex.Matches(text))
            {
                
                if (string.IsNullOrEmpty(text)) return splitItems;
                if (string.IsNullOrEmpty("/"))
                {
                    splitItems.Add(matches.Value);
                    return splitItems;
                }
                int nextPos = 0;
                int curPos = 0;
                while (nextPos > -1)
                {
                    nextPos = matches.Value.IndexOf('/', curPos);
                    if (nextPos != -1)
                    {
                        splitItems.Add(matches.Value.Substring(curPos, nextPos - curPos));
                        curPos = nextPos + "/".Length;
                    }
                }
                splitItems.Add(text.Substring(curPos, matches.Value.Length - curPos));
            }
            return splitItems;
        }
    }
}
