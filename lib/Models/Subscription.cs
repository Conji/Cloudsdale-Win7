using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudsdaleWin7.lib.Models
{
    class Subscription
    {
        private static string _modelId { get; set; }
        private static string _subId { get; set; }

        /// <summary>
        /// Gets the subscription type of the model.
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }

        /// <summary>
        /// Sets the subscription Id of the model.
        /// </summary>
        public string SubscriptionId
        {
            get { return _subId; }
            set
            {
                if (_subId == value) return;
                _subId = SubscriptionType == SubscriptionType.Cloud
                             ? "CLOUD:" + _modelId
                             : "USER:" + _modelId;
            }
        }

        /// <summary>
        /// Sets the Id of the specified object.
        /// </summary>
        public string ModelId
        {
            get { return _modelId; }
            set { _modelId = value; }
        }
        
    }
    enum SubscriptionType
    {
        User = 0, 
        Cloud = 1
    }
}
