using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiHost.Auth
{
    public class RoleValidator
    {
        private const string NAME_ID_CLAIM_KEY = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public async Task OnTokenValidated(TokenValidatedContext context)
        {
            // identify user from validated token
            if (context.SecurityToken is JwtSecurityToken accessToken
                && context.Principal.Identity is ClaimsIdentity identity
                && identity.Claims != null)
            {
                // fetch roles for user based on some logic
                var assigneRole = identity.Claims.Any(x =>
                    x.Type == NAME_ID_CLAIM_KEY
                    && x.Value == "1234567890") ?
                    "Administrator" : "User";

                // assign role claims to user identity
                identity.AddClaim(
                    new Claim(ClaimTypes.Role, assigneRole, ClaimValueTypes.String));
            }

            await Task.Run(() => { });
            return;
        }
    }
}
