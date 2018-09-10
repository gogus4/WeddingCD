using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Exceptions
{
    /// <summary>
    /// Thrown when an authorization token is expired.
    /// </summary>
    [Serializable]
    public class AuthorizationTokenExpiredException : WeddingCDException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationTokenExpiredException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AuthorizationTokenExpiredException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationTokenExpiredException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public AuthorizationTokenExpiredException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationTokenExpiredException"/> class.
        /// </summary>
        public AuthorizationTokenExpiredException()
        {
        }
    }
}