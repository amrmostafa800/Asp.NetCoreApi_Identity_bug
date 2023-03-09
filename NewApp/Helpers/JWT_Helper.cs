using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewApp.Helpers
{
    public class JWT_Helper
    {
        public static string Generate(IConfiguration configuration, string username)
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim("username",username)
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: cred
                );

            var JWT = new JwtSecurityTokenHandler().WriteToken(Token);
            return JWT;
        }
    }
}
