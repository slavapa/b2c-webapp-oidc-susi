using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApp_OpenIDConnect_DotNet_B2C.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // You can use the PolicyAuthorize decorator to execute a certain policy if the user is not already signed into the app.
        [Authorize]
        public ActionResult Claims()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Username = userClaims?.FindFirst("preferred_username")?.Value;
            ViewBag.Email = userClaims?.FindFirst("email")?.Value;
            var emailsCol = userClaims?.FindAll("emails");

            if (string.IsNullOrEmpty(ViewBag.Email) && emailsCol != null && emailsCol.Count() > 0)
            {
                ViewBag.Email = emailsCol.Last().Value;
            }

            if (string.IsNullOrEmpty(ViewBag.Email))
            {
                ViewBag.Email = string.Empty;
            }

            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;
            //ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;

            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View("Error");
        }
    }
}