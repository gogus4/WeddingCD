using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeddingCD.Common.Extensions
{
    /// <summary>
    /// Extensions method for Exception
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Flattens the inner exception, i.e. returns a flattened list of exceptions from the InnerException property (recursively).
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// The flattened list of exceptions
        /// </returns>
        public static IEnumerable<Exception> FlattenInnerException(this Exception exception)
        {
            if (exception == null)
            {
                yield break;
            }

            var innerException = exception;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}