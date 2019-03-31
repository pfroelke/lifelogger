using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using LifeLogger.Commons;
using LifeLogger.ViewModels;
using LifeLogger.Models.Entity;
using LifeLogger.Models.Context;

namespace LifeLogger.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly LifeLoggerDbContext _context;
        private readonly IConfiguration _config;

        public UserController(LifeLoggerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Index()
        {
            return Json(await _context.Users.ToListAsync());
        }

        [HttpGet("Details")]
        public IActionResult Details()
        {
            return Content("<xml>This is poorly formatted xml. Details</xml>", "text/xml");
        }

        [HttpPost("Register")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel user)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                // if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                _context.Add((User)user);
                await _context.SaveChangesAsync();
                response = Ok("Account created!");
            }
            return response;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel user)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                User userFound = null;
                userFound = await _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();

                if (userFound != null)
                {
                    if (userFound.PasswordHash == PasswordHasher.HashPassword(user.Password, userFound.Salt))
                    {
                        var tokenString = BuildToken(userFound);
                        response = Ok(new { auth_token = tokenString });
                    }
                    else response = Unauthorized("Wrong Password!");
                }
                else response = Unauthorized("User not found!");
            }
            return response;
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            var claimsList = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims: claimsList,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
