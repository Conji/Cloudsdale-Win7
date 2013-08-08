using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Cloudsdale_Win7.Win7_Lib.Models;
using Newtonsoft.Json;
using Cloudsdale_Win7.Win7_Lib;

namespace Cloudsdale_Win7.Win7_Lib.Cloudsdale_Lib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CloudsdaleModel : INotifyPropertyChanged
    {
        public CloudsdaleModel()
        {
            UIMetadata = new UIMetadata(this);
        }

        /// <summary>
        /// Copies all the non-null properties of this model 
        /// marked with JsonProperty attributes to another model
        /// </summary>
        /// <param name="other"></param>
        public virtual void CopyTo(CloudsdaleModel other)
        {
            var properties = GetType().GetRuntimeProperties();
            var targetType = other.GetType();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
                if (attribute == null) continue;

                var value = property.GetValue(this);
                if (value != null)
                {
                    var targetProperty = targetType.GetRuntimeProperty(property.Name);
                    targetProperty.SetValue(other, value);
                }
            }
        }

        /// <summary>
        /// Metadata useful for UI display, provided by a MetadataProvider
        /// </summary>
        public UIMetadata UIMetadata { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            if (ModelSettings.Dispatcher != null && !ModelSettings.Dispatcher.Thread.IsAlive)
            {
                ModelSettings.Dispatcher.InvokeAsync(() => handler(this, new PropertyChangedEventArgs(propertyname)),
                                                     DispatcherPriority.Normal);
            }
            else
            {
                handler(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
