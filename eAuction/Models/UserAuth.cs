using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAuction.Models
{
    public class UserAuth
    {
        //readonly string key = "";
        public UserAuth(IConfiguration m_config)
        {
            //key = m_config.GetSection("AppSettings:TokenKey").Value;
        }
        public string GetToken(UserModel user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("B?E(H+KbPeShVmYq3t6w9z$C&F)J@NcQfTjWnZr4u7x!A%D*G-KaPdSgVkXp2s5v"));
            var token = new JwtSecurityToken(
            null,
            null,
            new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            },
            expires: DateTime.Now.AddMonths(12), // Token valid for 12 month.
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature));
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
