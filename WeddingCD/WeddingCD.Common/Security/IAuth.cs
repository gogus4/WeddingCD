using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Security
{
    /// <summary>
    /// Interface for authentication
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// Does the authentication.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="remember">if set to <c>true</c> remember authentication accross browsers.</param>
        void DoAuth(string username, bool remember);

        /// <summary>
        /// Signs out.
        /// </summary>
        void SignOut();
    }
}