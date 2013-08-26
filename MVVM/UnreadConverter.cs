﻿using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using CloudsdaleWin7.lib.CloudsdaleLib;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.MVVM
{
    class UnreadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cloudId = value.ToString();
            if (MessageSource.GetSource(cloudId).UnreadMessages == 0) return "";
            return MessageSource.GetSource(cloudId).UnreadMessages;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new ValueUnavailableException("Could not retrieve unread message list!");
        }
    }
}
