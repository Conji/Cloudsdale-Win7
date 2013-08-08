using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloudsdale_Win7.Providers;

namespace Cloudsdale_Win7.Cloudsdale
{
    /// <summary>
    /// Metadata linked to objects for databinding convenience
    /// </summary>
    public class UIMetaData
    {
        private readonly Dictionary<string, IMetadataObject> objects = new Dictionary<string, IMetadataObject>();
        private readonly CloudsdaleModel model;
        public UIMetaData(CloudsdaleModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// The metadata object for this provider key
        /// </summary>
        /// <param name="key">Metadata provider key</param>
        /// <returns>A metadata object</returns>
        public IMetadataObject this[string key]
        {
            get
            {
                return objects.ContainsKey(key)
                           ? objects[key]
                           : objects[key] = Cloudsdale.MetadataProviders[key].CreateNew(model);
            }
        }
    }
}
