using System;
using System.Globalization;
using System.Windows.Data;
using CloudsdaleWin7.lib.CloudsdaleLib;

namespace CloudsdaleWin7.MVVM
{
    class UnreadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var cloud in MainWindow.Instance.CloudList.Items)
            {
                if (cloud.ToString() != MainWindow.CurrentCloud["name"].ToString())
                {
                    var read = CloudView.Instance.ChatMessages.Items.Count;
                    var total = MessageSource.GetSource(cloud.ToString()).Messages.Count;
                    return total - read;
                }
                return 0;
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new ValueUnavailableException("Could not retrieve unread message list!");
        }
    }
}
