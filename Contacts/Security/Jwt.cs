using Contacts.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contacts.Security
{
    public class Jwt : IJwt
    {
        private readonly IConfiguration _configuration;

        public Jwt(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtToken GenerateJwtToken(string username)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new Exception("JWT Key not found"));
                var keyExpiryMinutes = _configuration["Jwt:ExpiryMinutes"];
                var expirationDate = DateTime.UtcNow.AddMinutes(int.Parse(keyExpiryMinutes ?? throw new Exception("JWT ExpiryMinutes not found")));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                    Expires = expirationDate,
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return new JwtToken
                {
                    Token = tokenString
                };
            }
            catch
            {
                return new JwtToken
                {
                    Token = string.Empty
                };
            }
        }
    }
}