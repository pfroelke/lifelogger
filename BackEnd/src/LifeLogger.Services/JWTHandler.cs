using System;
using System.IO;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using LifeLogger.Models.Entity;
using LifeLogger.Commons;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LifeLogger.Services
{
    public interface IJWTHandler
    {
        void AddAuthentification(IServiceCollection services, IConfiguration configuration);
        TokenValidationParameters Parameters { get; }
        JWT CreateToken(User user);
        IEnumerable<Claim> GetTokenClaims(string token);
        string GetUserIdFromToken(string token);
    }

    public class JWTHandler : IJWTHandler
    {
        private readonly IConfiguration _config;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly string _issuer;
        private readonly string _audience;
        private SigningCredentials _signingCredentials;
        private SecurityKey _issuerSigningKey;
        public TokenValidationParameters Parameters { get; private set; }

        public JWTHandler(IConfiguration configuration)
        {
            _config = configuration;
            _issuer = _config["Jwt:Issuer"] ?? string.Empty;
            _audience = _config["Jwt:Audience"] ?? string.Empty;
            InitializeRsa();
            InitializeJwtParameters();
        }

        private void InitializeRsa()
        {
            if (string.IsNullOrWhiteSpace(_config["Jwt:RSAPrivateKeyXml"]) ||
                string.IsNullOrWhiteSpace(_config["Jwt:RSAPublicKeyXml"]))
            {
                return;
            }
            using (RSA publicRsa = RSA.Create())
            {
                var publicKeyXml = File.ReadAllText(_config["Jwt:RSAPublicKeyXml"]);
                publicRsa.RSAFromXML(publicKeyXml);
                _issuerSigningKey = new RsaSecurityKey(publicRsa);
            }
            using (RSA privateRsa = RSA.Create())
            {
                var privateKeyXml = File.ReadAllText(_config["Jwt:RSAPrivateKeyXml"]);
                privateRsa.RSAFromXML(privateKeyXml);
                var privateKey = new RsaSecurityKey(privateRsa);
                _signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
            }
        }

        public void AddAuthentification(IServiceCollection services, IConfiguration configuration)
        {
            using (RSA publicRsa = RSA.Create())
            {
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = Parameters;
                    options.SaveToken = true;
                });
            }
        }

        private void InitializeJwtParameters()
        {
            Parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = _issuerSigningKey
            };
        }

        public JWT CreateToken(User user)
        {
            var exp = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:ExpiryDays"]));
            var now = DateTime.UtcNow;
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwt = new JwtSecurityToken(_issuer,
                _audience,
                claims: claimsList,
                notBefore: now,
                expires: exp,
                signingCredentials: _signingCredentials);

            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JWT
            {
                Token = token,
                Expires = exp
            };
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Given Argument is null or empty.");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal ValidToken = jwtSecurityTokenHandler.ValidateToken(token, Parameters, out SecurityToken validatedToken);
                return ValidToken.Claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserIdFromToken(string token)
        {
            List<Claim> claims = GetTokenClaims(token).ToList();
            string userId = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            return userId;
        }
    }
}
