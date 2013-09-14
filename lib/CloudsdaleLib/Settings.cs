using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.CloudsdaleLib
{
    public class Settings
    {
        private static readonly string SettingsFile = CloudsdaleSource.SettingsFile;
        private static JObject _settings { get; set; }
        public Settings()
        {
            Initialize();
            var reader = new StreamReader(SettingsFile);
            _settings = new JObject(reader.ReadToEnd());
            reader.Close();
        }
        public string this[string key]
        {
            get
            {
                if (_settings[key] == null) return (String)_settings[key];
                AddSetting(key, "empty");
                return (String)_settings[key];
            }
        }
        
        private static void Initialize()
        {
            if (File.Exists(SettingsFile)) return;
            File.AppendAllText(SettingsFile, "{" + Environment.NewLine + 
                                             "}");
        }

        public void AddSetting(string tokenKey, object value)
        {
            _settings.Add(tokenKey, (JToken)value);
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
            File.WriteAllText(SettingsFile, _settings.ToString());
        }
    }
}
