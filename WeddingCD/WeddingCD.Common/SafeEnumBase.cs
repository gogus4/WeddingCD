using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common
{
    /// <summary>
    /// Base class for safe string enumerations.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration.</typeparam>
    public abstract class SafeEnumBase<T>
        where T : SafeEnumBase<T>
    {
        #region Private members

        /// <summary>
        /// The code.
        /// </summary>
        private readonly string code;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeEnumBase{T}"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        protected SafeEnumBase(string code)
        {
            this.code = code;
        }

        /// <summary>
        /// Gets the Code value
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }
        }

        /// <summary>
        /// Gets all values of the enumeration.
        /// </summary>
        /// <returns>The values.</returns>
        public static IEnumerable<T> GetAllValues()
        {
            var properties = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var property in properties)
            {
                yield return (T)property.GetValue(null);
            }
        }

        /// <summary>
        /// Parse a safe enum from a string
        /// </summary>
        /// <param name="code">the string to parse</param>
        /// <returns>the parsed value if parsing is ok, null otherwise</returns>
        public static T Parse(string code)
        {
            return GetAllValues().FirstOrDefault(z => z.Code == code);
        }

        /// <summary>
        /// Get a safe enum from its Label
        /// </summary>
        /// <param name="label">the label</param>
        /// <returns>the value.</returns>
        public static T GetLabel(string label)
        {
            return (T)typeof(T).GetField(label).GetValue(null);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.code;
        }
    }
}