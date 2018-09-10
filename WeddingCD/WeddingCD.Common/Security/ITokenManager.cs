using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Security
{
    /// <summary>
    /// Interface that must be implemented by classes that manage tokens.
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Sets the authentication token.
        /// </summary>
        /// <param name="token">The token.</param>
        void SetAuthenticationToken(string token);
    }
}