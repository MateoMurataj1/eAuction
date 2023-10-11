using eAuction.Data;
using eAuction.Data.Services;
using eAuction.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAuction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly IAccountsService _serviceAccount;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public ProductsController(IProductsService service, IConfiguration config, IAccountsService serviceAccount, AppDbContext context)
        {
            _service = service;
            _config = config;
            _context = context;

        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {

            string token1 = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Get the current user's claims
            var userClaims = User.Claims.ToList();

            // Retrieve specific claims by their claim type
            var usernameClaim = User.FindFirst(ClaimTypes.Name);
            var roleClaims = User.FindAll(ClaimTypes.Role);

            var allProducts = await _service.GetAllAsync();
            return View(allProducts);

        }

        //[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {

            product.StartingDate = DateTime.Now;
            product.SellerName = "useri121";
            product.SellerId = 1234;
            product.HighestBid = product.StartingBid;
            product.HighestBidName = "userii2";

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null) return View("Empty");
            return View(productDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id, ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null) return View("Empty");
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null) return View("Empty");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel user)
        {

            //await _service.Login(user);

            var isValid = await _context.Users.AnyAsync(n => n.Username == user.Username && n.Password == user.Password);
            if (isValid)
            {
                var result = await _context.Users.FirstOrDefaultAsync(n => n.Username == user.Username);

                var token = GenerateToken(user);

                return Ok(token);

                //return RedirectToAction("Index", "Products");

            }
            else
            {
                return View();
            }

        }

        // To generate token
        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username)

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
