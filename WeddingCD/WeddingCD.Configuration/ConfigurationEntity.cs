using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace WeddingCD.Configuration.Azure
{
    /// <summary>
    /// Entity to store configuration in Azure cloud storage.
    /// </summary>
    public class ConfigurationEntity : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationEntity"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public ConfigurationEntity(string key, string value)
        {
            this.PartitionKey = "Conf";
            this.RowKey = key;

            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationEntity"/> class.
        /// </summary>
        public ConfigurationEntity()
        {
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}