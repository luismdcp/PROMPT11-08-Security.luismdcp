using System;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;

namespace IdentityProvider.Models
{
    public class SimpleSecurityTokenService : SecurityTokenService
    {
        private readonly SimpleSecurityTokenServiceConfiguration config;

        public SimpleSecurityTokenService(SimpleSecurityTokenServiceConfiguration config) : base(config)
        {
            this.config = config;
        }

        protected override IClaimsIdentity GetOutputClaimsIdentity(IClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
        {
            throw new NotImplementedException();
        }

        protected override Scope GetScope(IClaimsPrincipal principal, RequestSecurityToken request)
        {
            throw new NotImplementedException();
        }
    }
}