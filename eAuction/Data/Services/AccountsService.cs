using eAuction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAuction.Data.Services
{
    public class AccountsService: IAccountsService
    {
        private readonly AppDbContext _context;

        public AccountsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Login(UserModel user)
        {
            var isValid = await _context.Users.AnyAsync(n => n.Username == user.Username && n.Password == user.Password);
            //if (isValid)
            //{
            //    var result = await _context.Users.FirstOrDefaultAsync(n => n.Username == user.Username);

            //    var key = Encoding.ASCII.GetBytes("B?E(H+KbPeShVmYq3t6w9z$C&F)J@NcQfTjWnZr4u7x!A%D*G-KaPdSgVkXp2s5v");
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new[]
            //        {
            //            new Claim(ClaimTypes.Name, user.Username)
            //        }),
            //        Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    var token = tokenHandler.CreateToken(tokenDescriptor);
            //    var tokenString = tokenHandler.WriteToken(token);

            //    return Ok(new { Token = tokenString });

            //}
        }

        public async Task Logout()
        {
            var result =  _context.Users.FirstOrDefaultAsync(n => n.Username == "1");
        }

        public async Task Register(UserModel user)
        {
            // Check if the username is already taken
            bool isUsernameTaken = await _context.Users.AnyAsync(u => u.Username == user.Username);

            if (!isUsernameTaken)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                return ;
            }

        }
    }
}
