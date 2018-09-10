using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Exceptions
{
    /// <summary>
    /// Represents a daabase access exception.
    /// </summary>
    [Serializable]
    public class DatabaseAccessException : TechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAccessException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DatabaseAccessException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAccessException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public DatabaseAccessException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAccessException"/> class.
        /// </summary>
        public DatabaseAccessException()
        {
        }
    }
}