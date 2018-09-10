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
    public abstract class SafeEnumWithDescriptionBase<T> : SafeEnumBase<T>
        where T : SafeEnumWithDescriptionBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeEnumWithDescriptionBase{T}" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        protected SafeEnumWithDescriptionBase(string code, string description)
            : base(code)
        {
            this.Description = description;
        }

        /// <summary>
        /// Gets the Code value
        /// </summary>
        public string Description { get; private set; }
    }
}