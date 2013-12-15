using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc.Commands
{
    public static class CommandManager
    {
        public static List<ICommand> Commands = new List<ICommand>();

        public static string[] ParseToCommandArgs(this string input)
        {
            return input.Split(' ');
        }

        public static void TryExecuteCommand(string input)
        {
            var cache = input.Replace("/", "").ToLower().Split(' ')[0];
            foreach (var command in Commands.Where(c => c.Name == cache || c.Aliases.Contains(cache)))
                command.Execute(input.Split(' ').Skip(1).ToArray());
        }
    }
}
