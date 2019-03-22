using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LifeLogger.Models.Entity;
using LifeLogger.Models.Context;
using LifeLogger.ViewModels;

namespace LifeLogger.Web.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class UserController : Controller
    {
        private readonly LifeLoggerDbContext _context;

        public UserController(LifeLoggerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Content("<xml>This is poorly formatted xml. Index</xml>", "text/xml");
        }

        [HttpGet("Details")]
        public IActionResult Details()
        {
            return Content("<xml>This is poorly formatted xml. Details</xml>", "text/xml");
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Register")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                // var user = _mapper.Map(model);
                //var result = await _userManager.CreateAsync(user, model.Password);

                // if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                _context.Add((User)model);
                await _context.SaveChangesAsync();
                response = Ok("Account created!");
            }
            return response;
        }
    }
}
