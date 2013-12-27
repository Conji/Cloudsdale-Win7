using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7
{
    public static class Changelog
    {
        public static string Version { get { return ClientVersion.Version; } }

        public static string[] Changes
        {
            get
            {
                return new[]
                       {
                           "Added shortcuts for navigation throught the app.",
                           "UI is more responsive and doesn't slow down at cloud change.",
                           "Added home screen actions."
                       };
            }
        }
    }
}
