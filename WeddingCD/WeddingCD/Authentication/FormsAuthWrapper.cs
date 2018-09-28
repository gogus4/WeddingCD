using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Thermibox.Common.Security;

namespace Thermibox.Web.Common.Authentication
{
    /// <summary>
    /// Wrapper for FormsAuthentication
    /// </summary>
    public class FormsAuthWrapper : IAuth
    {
        /// <summary>
        /// Does the authentication.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="remember">if set to <c>true</c> remember authentication accross browsers.</param>
        public void DoAuth(string username, bool remember)
        {
            FormsAuthentication.SetAuthCookie(username, remember);
        }

        /// <summary>
        /// Signs out.
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}