using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingCD.Models
{
    /// <summary>
    /// Base class for ViewModels
    /// </summary>
    public abstract class ViewModelBase
    {
        /// <summary>
        /// Gets a value indicating whether the user is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user is logged in; otherwise, <c>false</c>.
        /// </value>
        public static bool IsUserLoggedIn
        {
            get
            {
                return HttpContext.Current != null
                    && HttpContext.Current.Session != null
                    && HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
    }
}