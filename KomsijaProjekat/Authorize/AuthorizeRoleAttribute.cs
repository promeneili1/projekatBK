using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomsijaProjekat.Authorize
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly UserRole[] allowedRoles;

        public AuthorizeRoleAttribute(params UserRole[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            var userRole = httpContext.Session["UserRole"]?.ToString();

            if (Enum.TryParse(userRole, out UserRole role))
            {
                return allowedRoles.Contains(role);
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            
            filterContext.Result = new ViewResult
            {
                ViewName = "Unauthorized", 
                ViewData = new ViewDataDictionary
                {
                    { "ErrorMessage", "Niste autorizovani za ovu akciju." }
                }
            };
        }
    }
}
