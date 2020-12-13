using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TokenAuthentication;

namespace Greeting.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;
        private int expiryTime;

        public TokenManager(IConfiguration configuration)
        {
            tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes(configuration.GetSection("Jwt")["AccessSecretKey"]);
            expiryTime = Convert.ToInt32(configuration.GetSection("Jwt")["ExpiryTime"]);

        }

        public string Encode(Account account)
        {
            return Encode(account, expiryTime);
        }

        public string Encode(Account account, int ExpiryTimeInMinutes, byte[] secret=null)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.UserData, account.AccountId.ToString()),
                    new Claim(ClaimTypes.Email, account.Email)  
                }),
                Expires = DateTime.UtcNow.AddMinutes(ExpiryTimeInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret == null ? secretKey : secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal Decode(string token)
        {
            return Decode(token, secretKey);
        }


        public ClaimsPrincipal Decode(string token, byte[] secret)
        {
            var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.FromMinutes(0)
            }, out SecurityToken validatedToken);
            return claims;
        }
    }
}
