using System;

namespace CloudsdaleWin7.lib.ErrorConsole.CConsole
{
    public class WriteError
    {
        public static void Write(string error)
        {
            var messageFormat = "[" + DateTime.Now.ToString() + "] " + error + Environment.NewLine;
            ErrorConsole.Instance.ConsoleText.Text += messageFormat;
        }
    }
}
