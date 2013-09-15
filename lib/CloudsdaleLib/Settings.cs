using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib
{
    public class Settings
    {
        private static readonly string SettingsFile = CloudsdaleSource.SettingsFile;
        private static JObject _settings = new JObject();
        public Settings()
        {
            Initialize();
            _settings = new JObject(SettingsObject);
        }
        public string this[string key]
        {
            get
            {
                //return (String)_settings["settings"][key];
                if (_settings[key] != null) return (String) _settings[key];
                _settings.Add(key, key);
                return (String) _settings[key];
            }
        }
        
        private static void Initialize()
        {
            if (File.Exists(SettingsFile)) return;
            var jo = new JObject();
            jo["settings"] = new JObject();
            File.WriteAllText(SettingsFile, jo.ToString());
        }

        public void AddSetting(string tokenKey, string value)
        {
            _settings.Add(tokenKey, value);
        }
        public void AddSetting(string tokenKey, bool value)
        {
            _settings.Add(tokenKey, value);
        }
        public void ChangeSetting(string tokenKey, object value)
        {
            _settings[tokenKey].Replace((String)value);
        }
        public void RemoveSetting(string tokenKey)
        {
            _settings[tokenKey].Remove();
        }
        public void Save()
        {
            var saveObject = new JObject();
            saveObject["settings"] = new JObject(_settings);
            File.WriteAllText(SettingsFile, saveObject.ToString());
        }
        private static JObject SettingsObject
        {
            get
            {
                var o = JObject.Parse(File.ReadAllText(SettingsFile));
                return (JObject)o["settings"];
            }
        }
    }
}
