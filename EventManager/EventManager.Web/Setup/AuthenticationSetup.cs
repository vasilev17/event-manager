using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventManager.Web.Setup
{
    public static class AuthenticationSetup
    {
        public static void SetupAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            //Get the signing key from the settings
            var signingKey = configuration
                        .GetSection("Jwt")
                        .GetSection("SigningKey")
                        .Value;

            if (signingKey == null)
                throw new ApplicationException("No signing key is set up");

            var signkeyBits = Encoding.ASCII.GetBytes(signingKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(signkeyBits),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }
    }
}
