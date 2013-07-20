using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cloudsdale.connection;
using Cloudsdale.lib.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public static string AkaList(string UserID)
        {
            JObject user = UserJson(UserID);
            return user["result"]["also_known_as"].ToString().MultiReplace("[", "]", Environment.NewLine, "").Trim();
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
            return self["user"]["username_changes_allowed"].ToObject<int>();
        }

        public async Task UploadNewAvatar(Stream picture, string mimetype)
        {
            HttpContent postData;
            using (var dataStream = new MemoryStream())
            {
                using (picture)
                {
                    picture.CopyToAsync(dataStream);
                }
                postData = new MultipartFormDataContent("--" + Guid.NewGuid() + "--")
                               {
                                   new ByteArrayContent(dataStream.ToArray())
                                       {
                                           Headers =
                                               {
                                                   ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                                                            {
                                                                                Name = "user[avatar]",
                                                                                FileName = "GenericImage.png"
                                                                            },
                                                   ContentLength = dataStream.Length,
                                               }
                                       }
                               };
                var request = new HttpClient()
                                  {
                                      DefaultRequestHeaders =
                                          {
                                              {"Accept", "application/json"},
                                              {"X-Auth-Token", Token.ReadToken()}
                                          }
                                  };
            }

        }
    }
}