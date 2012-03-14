using System.Web.Mvc;
using Microsoft.IdentityModel.Web;

namespace MvcApplication_ExplicitWIF.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/LogOn
        public ActionResult LogOn(string returnUrl)
        {
            var signin = FederatedAuthentication.WSFederationAuthenticationModule.CreateSignInRequest("1", returnUrl, false);
            return Redirect(signin.WriteQueryString());
        }

        public ActionResult LogOff()
        {
            FederatedAuthentication.WSFederationAuthenticationModule.SignOut(false);
            return RedirectToAction("Index", "Home");
        }
    }
}