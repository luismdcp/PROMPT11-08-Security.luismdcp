using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.SecurityTokenService;

namespace IdentityProvider.Models
{
    public class SimpleSecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {
        public SimpleSecurityTokenServiceConfiguration()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var certificate = store.Certificates.OfType<X509Certificate2>().FirstOrDefault(c => c.Subject.Contains("idp.prompt11.local"));

            this.SigningCredentials = new X509SigningCredentials(certificate);
            this.TokenIssuerName = "idp.prompt11.local";
        }
    }
}