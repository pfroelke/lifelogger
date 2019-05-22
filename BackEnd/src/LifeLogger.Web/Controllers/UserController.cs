using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using LifeLogger.Commons;
using LifeLogger.ViewModels;
using LifeLogger.Models.Entity;
using LifeLogger.Models.Context;
using LifeLogger.Services;
using System;

namespace LifeLogger.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly LifeLoggerDbContext _context;
        private readonly IConfiguration _config;
        private readonly IJWTHandler _JWTHandler;
        private readonly IUserService _userService;

        public UserController(LifeLoggerDbContext context, IConfiguration configuration, IJWTHandler JWTHandler, IUserService userService)
        {
            _context = context;
            _config = configuration;
            _JWTHandler = JWTHandler;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Json(await _context.Users.ToListAsync());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel user)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                await _userService.AddUserAsync(user);
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
                User userFound = await _userService.GetUserByNameAsync(user.UserName);

                if (userFound != null)
                {
                    if (userFound.PasswordHash == PasswordHasher.HashPassword(user.Password, userFound.Salt))
                    {
                        JWT jwt = _JWTHandler.CreateToken(userFound);
                        UserViewModel vm = userFound;
                        response = Ok(value: new { jwt, user = vm });
                    }
                    else response = Unauthorized("Wrong Password!");
                }
                else response = Unauthorized("User not found!");
            }
            return response;
        }

        [HttpGet("isLoggedIn"), Authorize]
        public IActionResult IsLoggedIn()
        {
            return Ok();
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                User userFound =  await _userService.GetUserByIdAsync(id);

                if (userFound != null)
                {
                    response = Json((UserViewModel)userFound);
                }
                else response = NotFound(id);
            }
            return response;
        }

        [HttpPut("WorkConfig"), Authorize]
        public async Task<IActionResult> UpdateWorkSettings([FromBody] WorkConfigViewModel workConfig)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                string token = Request.Headers["Authorization"];
                // remove 'Bearer '
                token = token.Remove(0, 7);

                string userId = _JWTHandler.GetUserIdFromToken(token);

                User userFound = await _userService.GetUserByIdAsync(userId);
                if (userFound != null)
                {
                    userFound.CompanyName = workConfig.CompanyName;
                    userFound.JobTitle = workConfig.JobTitle;
                    userFound.IncomePerHour = workConfig.IncomePerHour;
                    await _userService.UpdateUserAsync(userFound);
                    response = Ok();
                }
                else response = NotFound(userId);
            }
            return response;
        }
    }
}
