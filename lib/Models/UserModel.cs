using System.IO;
using System.Net;
using System.Threading.Tasks;
using Cloudsdale.connection;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Cloudsdale.lib.Models
{
    public sealed class UserModel
    {
        public static string[] _akaList;

        public static bool NeedsToConfirmRegistration;
        public static bool NeedsPasswordChange;
        public static bool NeedsNameChange;
        public static bool NeedsUsernameChange;
        public static bool NeedsEmailChange;
        public static bool HasReadTermsAndConditions;
        public static bool HasAvatar;
        public static bool IsMemberOfACloud;
        /// <summary>
        /// Initializer for fetching the user .json file.
        /// </summary>
        /// <param name="UserID">The user id of the requested user.json file.</param>
        /// <returns>string</returns>
        public static JObject UserJson(string UserID)
        {
            WebRequest request = WebRequest.Create(Endpoints.UserJson.Replace("[:id]", UserID));
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            var responseReader = new StreamReader(responseStream);
            var responseData = JObject.Parse(responseReader.ReadToEnd());
            
            return responseData;
        }
        /// <summary>
        /// Fetches the changeable name from the UserID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string Name(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["name"].ToString();
        }
        /// <summary>
        /// Fetches the static @name from the UserID. 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string Username(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["name"].ToString();
        }
        /// <summary>
        /// Fetches the timezone the user last logged in from.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string TimeZone(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["time_zone"].ToString();
        }
        /// <summary>
        /// Fetches the time that the user created the current account.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string MemberSince(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["member_since"].ToString();
        }
        /// <summary>
        /// Fetches the Skype username of the user.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string SkypeUsername(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["skype_name"].ToString();
        }
        /// <summary>
        /// Fetches the "also-known-as" list of the userID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<string> AkaList(string UserID)
        {
            AkaList(UserID).AddRange(_akaList);
            return AkaList(UserID);
        }
        public static string Status(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["status"].ToString();
        }
        public static string Email()
        {
            JObject self = Main.User;
            return self["user"]["email"].ToString();
        }
        public static string AuthToken()
        {
            JObject self = Main.User;
            return self["user"]["X-Auth-Token"].ToString();
        }

        public static int NameChangesAllowed()
        {
            JObject self = Main.User;
            return self["user"]["name-changes-allowed"].ToObject<int>();
        }
    }
}