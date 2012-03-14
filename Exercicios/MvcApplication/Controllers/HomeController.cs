using System.Linq;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Index()
        {
            var ident = this.User as IClaimsPrincipal;
            var emailClaim = ident.Identities[0].Claims.FirstOrDefault(c => c.ClaimType == ClaimTypes.Role);
            return "Hi there " + (emailClaim != null ? emailClaim.Value : "stranger");
        }
    }
}