using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LifeLogger.Models.Entity;
using LifeLogger.Models.Context;
using LifeLogger.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LifeLogger.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly LifeLoggerDbContext _context;

        public UserController(LifeLoggerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return Json(await _context.Users.Select(p => new { p.UserName, p.FirstName, p.Email, p.LastName, p.PasswordHash }).ToListAsync());
        }

        [HttpGet("Details")]
        public IActionResult Details()
        {
            return Content("<xml>This is poorly formatted xml. Details</xml>", "text/xml");
        }

        [HttpPost("Register")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                // if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                _context.Add((User)model);
                await _context.SaveChangesAsync();
                response = Ok("Account created!");
            }
            return response;
        }
    }
}
