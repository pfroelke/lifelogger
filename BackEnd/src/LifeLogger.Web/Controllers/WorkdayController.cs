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
    public class WorkdayController : Controller
    {
        private readonly LifeLoggerDbContext _context;
        private readonly IConfiguration _config;
        private readonly IJWTHandler _JWTHandler;
        private readonly IUserService _userService;
        private readonly IWorkdayService _workdayService;

        public WorkdayController(LifeLoggerDbContext context, IConfiguration configuration, IJWTHandler JWTHandler, IUserService userService, IWorkdayService workdayService)
        {
            _context = context;
            _config = configuration;
            _JWTHandler = JWTHandler;
            _userService = userService;
            _workdayService = workdayService;
        }

        [HttpPost("Create"), Authorize]
        public async Task<IActionResult> CreateWorkday([FromBody] WorkdayViewModel workday)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                User userFound = await _userService.GetUserByHeaderAsync(Request.Headers["Authorization"]);

                if (userFound != null)
                {
                    workday.IncPerHour = userFound.IncomePerHour;
                    Workday toAdd = workday;
                    toAdd.Owner = userFound;
                    await _workdayService.AddWorkdayAsync(toAdd);
                    response = Json((WorkdayViewModel)toAdd);
                }
                else response = NotFound();
            }
            return response;
        }

        [HttpDelete("Remove/{id}"), Authorize]
        public async Task<IActionResult> RemoveWorkday(string id)
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                Workday workdayFound = await _workdayService.GetWorkdayByIdAsync(id);
                User userFound = await _userService.GetUserByHeaderAsync(Request.Headers["Authorization"]);

                if (userFound != null && workdayFound != null &&
                    workdayFound.Owner == userFound)
                {
                    await _workdayService.RemoveWorkdayAsync(workdayFound);
                    response = Ok();
                }
                else response = NotFound();
            }
            return response;
        }

        [HttpGet("Workdays"), Authorize]
        public async Task<IActionResult> GetUsersWorkdays()
        {
            IActionResult response = BadRequest(ModelState);
            if (ModelState.IsValid)
            {
                User userFound = await _userService.GetUserByHeaderAsync(Request.Headers["Authorization"]);

                if (userFound != null)
                {
                    var workdays = _workdayService.GetUsersWorkdays(userFound);
                    workdays.ConvertAll(workday => (WorkdayViewModel)workday);
                    return Json(workdays);
                }
                else response = NotFound();
            }
            return response;
        }
    }
}
