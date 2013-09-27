namespace CloudsdaleWin7.lib
{
    class Endpoints
    {
        //Client
        #region v1
        /// <summary>
        /// Cloudsdale base endpoint
        /// </summary>
        public const string Base = "http://www.cloudsdale.org/";
        /// <summary>
        /// Cloudsdale version (Will be changed to v2 shortly).
        /// </summary>
        public const string Version = "v1";
        /// <summary>
        /// Image source for Cloudsdale.
        /// </summary>
        public const string AssetSource = "https://avatar-cloudsdale.netdna-ssl.com/";
        /// <summary>
        /// Retrieves the json file of the endpoint.
        /// </summary>
        private const string Json = ".json";

        /// <summary>
        /// Cloudsdale API base.
        /// </summary>
        public const string Api = Base + Version;
        /// <summary>
        /// Internal token for the server.
        /// </summary>
        public const string InternalToken = "$2a$10$7Pfcv89Q9c/9WMAk6ySfhu";

        /// <summary>
        /// Session endpoint of the logged in user.
        /// </summary>
        public const string Session = Api + "/sessions";
        /// <summary>
        /// User endpoint.
        /// </summary>
        public const string User = Api + "/users/[:id]";
        /// <summary>
        /// Cloud endpoint.
        /// </summary>
        public const string Cloud = Api + "/clouds/[:id]";
        /// <summary>
        /// Cloud JSON endpoint.
        /// </summary>
        public const string CloudJson = Cloud + Json;
        /// <summary>
        /// User JSON endpoint.
        /// </summary>
        public const string UserJson = User + Json;


        /// <summary>
        /// Cloud messages endpoint.
        /// </summary>
        public const string CloudMessages = Cloud + "/chat/messages";
        /// <summary>
        /// Retrieves all users registered on the cloud.
        /// </summary>
        public const string CloudUsers = Cloud + "/users";
        /// <summary>
        /// Retrieves all online users on the cloud.
        /// </summary>
        public const string CloudOnlineUsers = CloudUsers + "/online";

        /// <summary>
        /// Retrieves the ban list.
        /// </summary>
        public const string CloudBan = Cloud + "/bans";
        /// <summary>
        /// Checks a ban on a specific user.
        /// Use 
        /// </summary>
        public const string CloudUserBans = Cloud + "/bans?offender_id=[:offender_id]";
        /// <summary>
        /// Endpoint for popular clouds.
        /// </summary>
        public const string ExplorePopular = Api + "/clouds/popular.json";
        /// <summary>
        /// Endpoint for recently made clouds.
        /// </summary>
        public const string ExploreRecent = Api + "/clouds/recent.json";
        /// <summary>
        /// Endpoint to retrieve avatars.
        /// </summary>
        public const string Avatar = AssetSource + "/[:type]/[:id].png?s=[:size]";
        /// <summary>
        /// Server push address.
        /// </summary>
        public const string PushAddress = "wss://push.cloudsdale.org/push";
        /// <summary>
        /// Base endpoint for cloud links.
        /// </summary>
        public const string BaseCloudAddress = "http://www.cloudsdale.org/clouds/";
        /// <summary>
        /// Endpoint to retrieve the launcher version.
        /// </summary>
        public const string VersionAddress = "https://raw.github.com/Conji/AppVersions/master/cloudsdale.txt";
        /// <summary>
        /// Endpoint to retrieve the client.
        /// </summary>
        public const string ClientAddress = "https://dl.dropbox.com/s/a5nm1dgvn6lrmtm/Cloudsdale.exe";
        #endregion

        

        #region methods

        public const string Destroy = "DELETE";
        public const string Update = "PUT";
        public const string Index = "POST";

        #endregion
    }
}
