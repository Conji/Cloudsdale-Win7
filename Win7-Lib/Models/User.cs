using System;
using System.IO;
using Cloudsdale_Win7.Cloudsdale;
using Cloudsdale_Win7.Win7_Lib;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7.Win7_Lib.Models
{
    internal class User : CloudsdaleModel
    {
        private string _name;
        private bool? _hasAnAvatar;
        private bool? _isMemberOfACloud;
        private bool? _hasReadTnc;
        private bool? _isRegistered;
        private bool? _isBanned;
        private string _suspensionReason;
        private DateTime? _suspendedUntil;
        private DateTime? _memberSince;
        private string[] _alsoKnownAs;
        private string _skypeName;
        private string _role;
        private Avatar _avatar;
        private string _username;

        private static JObject BaseObject(string id)
        {
            var reader = new StreamReader(Endpoints.UserJson.Replace("[:id]", id));
            var response = (JObject) reader.ReadToEndAsync().ToString();
            reader.Close();
            reader.Dispose();
            return (JObject) response["user"];
        }

        private static JObject BaseSelf(string id)
        {
            var reader = new StreamReader(Endpoints.UserJson.Replace("[:id]", id));
            var response = (JObject)reader.ReadToEndAsync().ToString();
            reader.Close();
            reader.Dispose();
            return (JObject)response["result"];
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Skype
        {
            get { return (_skypeName != null ? _skypeName : null); }
            set
            {
                if (value == _skypeName) return;
                _skypeName = value;
                OnPropertyChanged();
            }
        }

        public static string AkaList(string UserID)
        {
            JObject user = BaseSelf(UserID);
            return user["also_known_as"].ToString().MultiReplace("[", "]", Environment.NewLine, "").Trim();
        }

        public static string Email()
        {
            JObject self = MainWindow.User;
            return self["user"]["email"].ToString();
        }

        public static string AuthToken()
        {
            JObject self = MainWindow.User;
            return self["user"]["auth_token"].ToString();
        }

        public static int NameChangesAllowed()
        {
            JObject self = MainWindow.User;
            return self["user"]["username_changes_allowed"].ToObject<int>();
        }

        public static bool HasReadTnc()
        {
            JObject self = MainWindow.User;
            return (bool) self["user"]["has_read_tnc"];
        }

        public static string Role(string userId)
        {
            JObject user = BaseSelf(userId);
            return user["role"].ToString();
        }

    }
}
