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
            // Proverava da li postoji korisnička uloga u sesiji
            var userRole = httpContext.Session["UserRole"]?.ToString();

            if (Enum.TryParse(userRole, out UserRole role))
            {
                return allowedRoles.Contains(role);
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Ako korisnik nije autorizovan, umesto preusmeravanja prikazujemo poruku na trenutnoj stranici
            filterContext.Result = new ViewResult
            {
                ViewName = "Unauthorized", // Ako imate View sa imenom "Unauthorized"
                ViewData = new ViewDataDictionary
                {
                    { "ErrorMessage", "Niste autorizovani za ovu akciju." }
                }
            };
        }
    }
}
