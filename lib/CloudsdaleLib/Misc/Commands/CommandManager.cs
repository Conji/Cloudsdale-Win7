using System.Collections.Generic;
using System.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc.Commands
{
    public static class CommandManager
    {
        public static List<ICommand> Commands = new List<ICommand>
                                                {
                                                    new SubscribeToCloud(),
                                                    new UnsubscribeToCloud()
                                                };

        public static string[] ParseToCommandArgs(this string input)
        {
            return input.Split(' ');
        }

        public static bool TryExecuteCommand(string input)
        {
            var i = 0;

            var cache = input.Replace("/", "").ToLower().Split(' ')[0];
            foreach (var command in Commands.Where(c => c.Name == cache || c.Aliases.Contains(cache)))
            {
                command.Execute(input.Split(' ').Skip(1).ToArray());
                i++;
            }
            return i != 0;
        }
    }
}
