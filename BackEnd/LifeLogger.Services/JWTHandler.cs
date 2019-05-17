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

namespace LifeLogger.Services
{
    public interface IJWTHandler
    {
        JWT CreateToken(User user);
    }

    public class JWTHandler : IJWTHandler
    {
        private readonly IConfiguration _config;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly string _issuer;
        private readonly string _audience;
        private SigningCredentials _signingCredentials;

        public JWTHandler(IConfiguration configuration)
        {
            _config = configuration;
            _issuer = _config["Jwt:Issuer"] ?? string.Empty;
            _audience = _config["Jwt:Audience"] ?? string.Empty;
            InitializeRsa();
        }

        private void InitializeRsa()
        {
            if (string.IsNullOrWhiteSpace(_config["Jwt:RSAPrivateKeyXml"]))
            {
                return;
            }
            using (RSA privateRsa = RSA.Create())
            {
                var privateKeyXml = File.ReadAllText(_config["Jwt:RSAPrivateKeyXml"]);
                privateRsa.RSAFromXML(privateKeyXml);
                var privateKey = new RsaSecurityKey(privateRsa);
                _signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
            }
        }

        public JWT CreateToken(User user)
        {
            var exp = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:ExpiryDays"]));
            var now = DateTime.UtcNow;
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
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
    }
}
