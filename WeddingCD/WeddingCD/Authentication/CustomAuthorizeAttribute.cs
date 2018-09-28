using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Thermibox.Web.Common.Authentication
{
    /// <summary>
    /// Attribute to associate functionalities to a given ASP.Net MVC action/controller.
    /// This allows authorization.
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="functionalities">The list functionalities to access to the controller/action.</param>
        public CustomAuthorizeAttribute(params string[] functionalities)
        {
            this.Functionalities = string.Join(",", functionalities);
        }

        /// <summary>
        /// Gets or sets the list of functionalities.
        /// </summary>
        /// <value>
        /// The list of functionalities.
        /// </value>
        public string Functionalities { get; set; }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            this.Roles = this.Functionalities;
            return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new HttpException((int)System.Net.HttpStatusCode.Forbidden, "Forbidden");
            }
            else
            {
                if (BaseMvcController.TypeUserWithoutSession == "SAV")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "IndexSAV" }));
                }
                else if (BaseMvcController.TypeUserWithoutSession == "Bailleur")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "IndexBailleur" }));
                }
                else if (BaseMvcController.TypeUserWithoutSession == "ELM")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "IndexELM" }));
                }
                else if (BaseMvcController.TypeUserWithoutSession == "ClientFinal")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }
    }
}