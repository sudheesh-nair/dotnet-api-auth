using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApiHost.Auth
{
    public class TokenValidator
    {
        private const string CONFIG_KEY_FOR_SECURITY_KEY = "SecurityKey";
        private TokenValidationParameters tokenValidationParameters;

        public TokenValidator(IConfiguration configuration)
        {
            var configuredSecurityKey = configuration.GetValue<string>(CONFIG_KEY_FOR_SECURITY_KEY);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuredSecurityKey));
            
            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return tokenValidationParameters;
        }
    }
}
