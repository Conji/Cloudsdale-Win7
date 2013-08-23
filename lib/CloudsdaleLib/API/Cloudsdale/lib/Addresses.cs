
namespace CloudsdaleWin7.lib.API.Cloudsdale.lib
{
    /// <summary>
    /// Endpoint class for API [V2]
    /// </summary>
    class Addresses
    {
        public const string ApiBase = "http://api.cloudsdale.org/v2/";
        public const string UserBranch = ApiBase + "users/$id/";
        public const string CloudBranch = ApiBase + "clouds/$id/";

        public const string Self = ApiBase + "me.json";


        #region Methods
        /// <summary>
        /// Accesses and changes the resource if allowed by the auth token.
        /// </summary>
        public readonly string Post = "POST";
        /// <summary>
        /// Retrieves the resource data.
        /// </summary>
        public readonly string Get = "GET";
        /// <summary>
        /// Not quite sure yet.... but I'll find out. :P
        /// </summary>
        public readonly string Options = "OPTIONS";

        #endregion
        /*allowed headers are X-Requested-With and X-Prototype-Version.
         * note: You will only use these headers if you use the "OPTIONS" method.
         * 
         */
    }
}
