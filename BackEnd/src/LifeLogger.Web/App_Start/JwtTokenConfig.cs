using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using LifeLogger.Commons;

namespace LifeLogger.Web.App_Start
{
    public class JWTTokenConfig
    {
        public static void AddAuthentification(IServiceCollection services, IConfiguration configuration)
        {
            using (RSA publicRsa = RSA.Create())
            {
                var publicKeyXml = File.ReadAllText(configuration["Jwt:RSAPublicKeyXml"]);
                publicRsa.RSAFromXML(publicKeyXml);

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new RsaSecurityKey(publicRsa)
                    };
                    options.SaveToken = true;
                });
            }
        }
    }
}
