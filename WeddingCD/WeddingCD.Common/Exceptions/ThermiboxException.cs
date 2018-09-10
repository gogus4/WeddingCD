using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Exceptions
{
    /// <summary>
    /// Represents a WeddingCD exception.
    /// </summary>
    [Serializable]
    public abstract class WeddingCDException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public WeddingCDException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public WeddingCDException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDException"/> class.
        /// </summary>
        public WeddingCDException()
        {
        }
    }
}