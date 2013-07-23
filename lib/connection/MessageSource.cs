﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace Cloudsdale.lib.connection
{
    public class MessageSource
    {
        private static readonly Dictionary<string, MessageSource> Sources = new Dictionary<string, MessageSource>();
        public readonly ObservableCollection<JToken> Messages = new ObservableCollection<JToken>();
        
        
        public static MessageSource GetSource(JToken cloud)
        {
            if (Sources.ContainsKey((string)cloud["id"]))
            {
                return Sources[(string)cloud["id"]];
            }
            return Sources[(string)cloud["id"]] = new MessageSource();
            
        }
        public static MessageSource GetSource(string cloudId)
        {
            if (Sources.ContainsKey(cloudId))
            {
                return Sources[cloudId];
            }
            return Sources[cloudId] = new MessageSource();
        }

        public void AddMessage(JToken message)
        {
            Messages.Add(message);
        }
    }
}