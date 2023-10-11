using eAuction.Data;
using eAuction.Data.Services;
using eAuction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAuction.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsService _service;
        private readonly AppDbContext _context;
        private readonly UserAuth _userAuth;
        public  JWTManage _jwt;
        private readonly IConfiguration _config;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountsController(IAccountsService service, AppDbContext context, IConfiguration config)
        {
            _service = service;
            _context = context;
            _config = config;

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await _service.Register(user);
            //return RedirectToAction(nameof(Login));
            return RedirectToAction("Login", "Products");
        }


        public ActionResult SignOut()
        {
            return View();
        }
    }
}
