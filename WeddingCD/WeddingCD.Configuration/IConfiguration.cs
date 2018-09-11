using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Configuration
{
    /// <summary>
    /// Interface for configuration access
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        string GetString(ConfigurationKey key);

        /// <summary>
        /// Gets the value as a Int16.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        short GetInt16(ConfigurationKey key);

        /// <summary>
        /// Gets the value as a Int32.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        int GetInt32(ConfigurationKey key);

        /// <summary>
        /// Gets the value as a Int64.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        long GetInt64(ConfigurationKey key);

        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        bool GetBoolean(ConfigurationKey key);

        /// <summary>
        /// Gets the value as a DateTime.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        DateTime GetDateTime(ConfigurationKey key);

        /// <summary>
        /// Gets the value as a DateTime.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        TimeSpan GetTimeSpan(ConfigurationKey key);

        /// <summary>
        /// Gets the value as an enumeration.
        /// </summary>
        /// <typeparam name="T">The type of enumeration</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The output value
        /// </returns>
        T GetEnumValue<T>(ConfigurationKey key) where T : struct;
    }
}