namespace CloudsdaleWin7.lib.CloudsdaleLib.Misc.Commands
{
    public class SubscribeToCloud : ICommand
    {
        public string Name { get; private set; }
        public string Usage { get; private set; }
        public string Definition { get; private set; }
        public string[] Aliases { get; private set; }

        public SubscribeToCloud()
        {
            Name = "subscribe";
            Usage = "/subscribe <cloud>";
            Aliases = new[] {"sub", "s", "watch"};
            Definition = "Subscribes current user to the cloud.";
        }

        public void Execute(string[] args)
        {
            App.Connection.MessageController.CurrentCloud.Cloud.IsSubscribed = true;
            App.Connection.NotificationController.Notification.Notify("Subscribed!");
        }
    }

    public class UnsubscribeToCloud : ICommand
    {
        public string Name { get; private set; }
        public string Usage { get; private set; }
        public string Definition { get; private set; }
        public string[] Aliases { get; private set; }

        public UnsubscribeToCloud()
        {
            Name = "unsubscribe";
            Usage = "/unsubscribe <cloud>";
            Definition = "Unsubscribes you from the cloud. Leave empty for current cloud.";
            Aliases = new[] {"u", "unsub", "stopwatch", "ignore"};
        }
        public void Execute(string[] args)
        {
            App.Connection.MessageController.CurrentCloud.Cloud.IsSubscribed = false;
            App.Connection.NotificationController.Notification.Notify("Unsubscribed");
        }
    }
}
