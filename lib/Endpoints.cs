namespace CloudsdaleWin7.lib
{
    class Endpoints
    {
        //Client
        public const string Base = "http://www.cloudsdale.org/";
        public const string Version = "v1";
        public const string AssetSource = "https://avatar-cloudsdale.netdna-ssl.com/";
        public const string Json = ".json";

        public const string Api = Base + Version;
        public const string InternalToken = "$2a$10$7Pfcv89Q9c/9WMAk6ySfhu";

        public const string Session = Api + "/sessions";
        public const string User = Api + "/users/[:id]";
        public const string Cloud = Api + "/clouds/[:id]";
        public const string CloudJson = Cloud + Json;
        public const string UserJson = User + Json;

        public const string CloudMessages = Cloud + "/chat/messages";
        public const string CloudUsers = Cloud + "/users";
        public const string CloudOnlineUsers = Cloud + "/online" + Json;

        public const string CloudUserBans = Cloud + "/bans?offender_id=[:offender_id]";
        public const string Avatar = AssetSource + "/[:type]/[:id].png?s=[:size]";
        public const string PushAddress = "wss://push.cloudsdale.org/push";

        public const string BaseCloudAddress = "http://www.cloudsdale.org/clouds/";

        public const string VersionAddress = "https://raw.github.com/Conji/AppVersions/master/cloudsdale.txt";
        public const string ClientAddress = "https://dl.dropbox.com/s/a5nm1dgvn6lrmtm/Cloudsdale.exe";

        #region methods

        public const string DESTROY = "DELETE";
        public const string UPDATE = "PUT";
        public const string INDEX = "POST";

        #endregion
    }
}
