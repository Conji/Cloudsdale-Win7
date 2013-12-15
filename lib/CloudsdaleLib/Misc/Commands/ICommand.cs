namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Usage { get; }
        string Definition { get; }
        string[] Aliases { get; }
        void Execute(string[] args);
    }
}