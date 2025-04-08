using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using projectSuperMarket.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectSuperMarket.Servies.Servies
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Suppliers supplier, string nameAdmin, string password)
        {
            List<Claim> claims = new List<Claim>();

            if (supplier != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, supplier.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, supplier.CompanyName));
                claims.Add(new Claim(ClaimTypes.Role, "Supplier"));
            }
            else if (nameAdmin == "admin" && password == "admin123")
            {
                claims.Add(new Claim(ClaimTypes.Name, nameAdmin));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                return null;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
