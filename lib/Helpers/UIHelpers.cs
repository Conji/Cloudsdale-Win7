using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.Flyouts.CloudFlyouts;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.lib.Helpers
{
    public static class UiHelpers
    {
        public static Hyperlink OnClickLaunch(this Hyperlink link, string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                uri = "http://" + uri;
            }

            link.Click += delegate
            {
                if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                {
                    MessageBox.Show(uri + " is not a well formed link! Please try another.");
                }
                BrowserHelper.FollowLink(uri);
            };
            return link;
        }

        public static void ShowFlyout(this User user)
        {
            Main.Instance.FlyoutFrame.Navigate(new UserFlyout(user));
        }

        public static void MessageOnSkype(string user)
        {
            Process.Start("skype:[:name]?chat".Replace("[:name]", user));
        }

        /// <summary>
        /// Allows a string search of the parameter.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ReplaceToQuery(this string request)
        {
            return request.Trim().Replace(" ", "%20");
        }

        /// <summary>
        /// Trims the string to the length paramter (by default of 200) and trails with '...'.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TrimToLength(this string input, int length = 200)
        {
            return input.Substring(0, length) + "...";
        }

        public static string FormatToFile(this string input)
        {
            return input + "_cd.png";
        }

        public static string CloudsdaleRandom(int length = 10)
        {
            var i = 0;
            var c = "";
            var r = new Random(DateTime.Now.Millisecond);

            while (i <= length)
            {
                c += r.Next(9);
                i++;
            }
            return c;
        }

        public static string TimeTitle()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");                
        }

        public static string ToLiteralString(this string[] array)
        {
            var s = "";
            if (array.Count() == 1) return String.Format("[ \"{0}\" ]", array[0]);

            var i = 0;
            foreach (var item in array)
            {
                if (i == 0)
                {
                    s = "[ \"" + item + "\", ";
                }
                else if(i == array.Length - 1)
                {
                    s += "\"" + item + "\" ]";
                }
                else
                {
                    s += "\"" + item + "\", ";
                }
                ++i;
            }
            return s;
        }

        public static string AddToLiteralString(this string[] array, object addition)
        {
            return "[ " + array.Aggregate("", (current, o) => current + (o + ", ")) + addition + " ]";
        }

        public static string[] Remove(this string[] array, object item)
        {
            return (array.Where(k => !item.Equals(k)).ToArray());
        }
    }
}
