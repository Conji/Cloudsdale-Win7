using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Cloudsdale_Win7.Cloudsdale;

namespace Cloudsdale_Win7.MVVM
{
    class UnreadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newint = (int) value;
            var source = MessageSource.GetSource(MainWindow.CurrentCloud["id"].ToString());
            int messageCount = source.Messages.Count;
            int readMessages = CloudView.Instance.ChatMessages.Items.Count;
            newint = messageCount - readMessages;

            return newint.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new ValueUnavailableException("Could not retrieve unread message list!");
        }
    }
}
