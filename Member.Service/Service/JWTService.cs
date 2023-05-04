using System;
using System.Runtime;
using Member.Service.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Member.Service.Service.Interfaces;

namespace Member.Service.Service.Service
{
	public class JWTService : IAuthorizationService
    {
        //public string GetJwtToken(int userID, JWTModel jWTModel)
        //{
        //    var secretKey = jWTModel.SecretKey;
        //    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        //    var claims = new[]
        //    {
        //        new Claim("userID", userID.ToString())
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescription = new SecurityTokenDescriptor()
        //    {
        //        Issuer = jWTModel.Issuer,
        //        Audience = jWTModel.Audience,
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddMinutes(jWTModel.ExpirationInMinutes),
        //        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescription);
        //    return tokenHandler.WriteToken(token);

        //}
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userID)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["SecretKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var claims = new[]
            {
                new Claim("userID", userID.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1000),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        public bool VerifyToken(string token)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["SecretKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true
                }, out SecurityToken validatedToken);
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }
    }
}
