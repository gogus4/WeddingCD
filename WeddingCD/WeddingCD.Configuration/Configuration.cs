using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingCD.Configuration;

namespace WeddingCD.Configuration
{
    /// <summary>
    /// Implementation of the configuration
    /// </summary>
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// The configuration cache
        /// </summary>
        private static Dictionary<string, string> configurationCache;

        /// <summary>
        /// Initializes static members of the <see cref="Configuration"/> class.
        /// </summary>
        static Configuration()
        {
            //var cloudStorage = AzureCloudStorage.GetAzureCloudStorage();

            //// Creates the Configuration table if they do not exist yet
            //var tableClient = cloudStorage.StorageAccount.CreateCloudTableClient();
            //var configurationTable = tableClient.GetTableReference("Configuration");
            //configurationTable.CreateIfNotExists();

            //// Creates the default configuration values
            //CreateDefaultConfiguration(configurationTable);

            //// Now gets every configuration value from cloud storage
            //configurationCache = configurationTable
            //    .CreateQuery<ConfigurationEntity>()
            //    .ToDictionary(z => z.Key, z => z.Value);

            //// Ensures every configuration bit is present
            //var requiredKeys = Enum.GetValues(typeof(ConfigurationKey))
            //    .Cast<ConfigurationKey>()
            //    .Select(z => z.ToString())
            //    .ToList();
            //var missingConf = requiredKeys.Where(z => !configurationCache.ContainsKey(z));
            //if (missingConf.Any())
            //{
            //    throw new TechnicalException(string.Format("Following configuration keys are missing: {0}", string.Join(", ", missingConf)));
            //}

            configurationCache = new Dictionary<string, string>();
            configurationCache.Add("DatabaseConnectionString", @"Server=SO1Z5377\SQLEXPRESS;Database=WeddingCD;Integrated Security=True;");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
        }

        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public string GetString(ConfigurationKey key)
        {
            return this.GetStringValue(key);
        }

        /// <summary>
        /// Gets the value as a Int16.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public short GetInt16(ConfigurationKey key)
        {
            short value;
            if (short.TryParse(this.GetStringValue(key), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            throw new ArgumentException(string.Format("The parameter {0} is not an int16", key));
        }

        /// <summary>
        /// Gets the value as a Int32.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public int GetInt32(ConfigurationKey key)
        {
            int value;
            if (int.TryParse(this.GetStringValue(key), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            throw new ArgumentException(string.Format("The parameter {0} is not an int32", key));
        }

        /// <summary>
        /// Gets the value as a Int64.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public long GetInt64(ConfigurationKey key)
        {
            long value;
            if (long.TryParse(this.GetStringValue(key), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            throw new ArgumentException(string.Format("The parameter {0} is not an int64", key));
        }

        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public bool GetBoolean(ConfigurationKey key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the value as a DateTime.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public DateTime GetDateTime(ConfigurationKey key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the value as a DateTime.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        public TimeSpan GetTimeSpan(ConfigurationKey key)
        {
            var valueAsString = this.GetStringValue(key);
            TimeSpan outValue;
            if (TimeSpan.TryParse(valueAsString, CultureInfo.InvariantCulture, out outValue))
            {
                return outValue;
            }

            throw new ArgumentException(string.Format("The specified key ({0}) doesn't represent a valid TimeSpan ({1})", key, valueAsString));
        }

        /// <summary>
        /// Gets the value as an enumeration.
        /// </summary>
        /// <typeparam name="T">The type of enumeration</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        /// <exception>
        /// The specified type {0} is not an enumeration
        /// or
        /// The specified key ({0}) doesn't represent a valid Enumeration ({1}) of type {2}
        /// </exception>
        public T GetEnumValue<T>(ConfigurationKey key) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("The specified type {0} is not an enumeration", typeof(T).FullName);
            }

            var valueAsString = this.GetStringValue(key);
            T outValue;
            if (Enum.TryParse(valueAsString, out outValue))
            {
                return outValue;
            }

            throw new ArgumentException(string.Format(
                "The specified key ({0}) doesn't represent a valid Enumeration ({1}) of type {2}",
                key,
                valueAsString,
                typeof(T).FullName));
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The value
        /// </returns>
        private string GetStringValue(ConfigurationKey key)
        {
            return this.GetStringValue(key.ToString());
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The value
        /// </returns>
        private string GetStringValue(string key)
        {
            return configurationCache[key];
        }
    }
}