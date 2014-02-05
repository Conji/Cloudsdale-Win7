using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.lib.Helpers
{
    public static class UiHelpers
    {
        public static void ShowFlyout(this User user)
        {
            Main.Instance.FlyoutFrame.Navigate(new UserFlyout(user));
        }

        public static void MessageOnSkype(string user)
        {
            Process.Start("skype:[:name]?chat".Replace("[:name]", user));
        }

        public static string ReplaceToQuery(this string request)
        {
            return request.Trim().Replace(" ", "%20");
        }

        public static string TrimToLength(this string input, int length = 200)
        {
            return input.Substring(0, length) + "...";
        }

        public static string FormatToFile(this string input)
        {
            return input + "_cd.png";
        }

        public static string TimeTitle()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");                
        }

        public static string[] Remove(this string[] array, object item)
        {
            return (array.Where(k => !item.Equals(k)).ToArray());
        }

        public static void SwitchVisibility(this UIElement controlA, UIElement controlB)
        {
            if (controlA.Visibility == controlB.Visibility)
                throw new InvalidDataException("Cannot swap visibility if already the same.");
            var vA = controlA.Visibility;
            var vB = controlB.Visibility;
            controlB.Visibility = vA;
            controlA.Visibility = vB;
        }
    }
}
