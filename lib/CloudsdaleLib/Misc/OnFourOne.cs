using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc
{
    /// <summary>
    /// Class for april fools day easter egg
    /// </summary>
    public static class OnFourOne
    {
        public static string ToPigLatin(this string input)
        {
            var charArray = input.Split(' ');
            var cacheString = charArray.Where(word => !word.StartsWith("a") && !word.StartsWith("e") && !word.StartsWith("i") && !word.StartsWith("o") && !word.StartsWith("u")).Aggregate("", (current, word) => current + (word.Substring(1) + word[0] + "ay "));
            return cacheString;
        }

        public static void NexLogin()
        {
            if (App.Connection.SessionController.CurrentSession.Username.ToLower() == "sexynexxy")
                App.Connection.NotificationController.Notification.Notify("Psssssst I love you :3");
        }
    }
}
