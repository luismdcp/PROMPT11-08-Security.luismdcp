using System.Web.Mvc;
using IdentityProvider.Models;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;

namespace IdentityProvider.Controllers
{
    public class FederationController : Controller
    {
        [Authorize]
        void Issue()
        {
            var req = WSFederationMessage.CreateFromUri(Request.Url);
            var resp = FederatedPassiveSecurityTokenServiceOperations.ProcessSignInRequest(req as SignInRequestMessage, 
                                                                                            User, 
                                                                                            new SimpleSecurityTokenService(new SimpleSecurityTokenServiceConfiguration()));

            resp.Write(Response.Output);
            Response.Flush();
            Response.End();
        }
    }
}