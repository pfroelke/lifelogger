using LifeLogger.Models.Context;
using LifeLogger.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace LifeLogger.Services
{
    public interface IWorkdayService
    {
        Task<string> AddWorkdayAsync(Workday workday);
        List<Workday> GetUsersWorkdays(User user);
        Task UpdateWorkdayAsync(Workday workday);
        Task RemoveWorkdayAsync(Workday workday);
        Task<Workday> GetWorkdayByIdAsync(string id);
    }
    public class WorkdayService : IWorkdayService
    {
        private readonly LifeLoggerDbContext _context;
        private readonly IConfiguration _config;
        private readonly IJWTHandler _JWTHandler;

        public WorkdayService(LifeLoggerDbContext context, IConfiguration configuration, IJWTHandler JWTHandler)
        {
            _context = context;
            _config = configuration;
            _JWTHandler = JWTHandler;
        }

        public async Task<string> AddWorkdayAsync(Workday workday)
        {
            _context.Add(workday);
            await _context.SaveChangesAsync();
            return workday.Id;
        }

        public List<Workday> GetUsersWorkdays(User user)
        {
            return _context.Workdays.Where(w => w.Owner == user).ToList();
        }

        public async Task UpdateWorkdayAsync(Workday workday)
        {
            _context.Add(workday);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task RemoveWorkdayAsync(Workday workday)
        {
            _context.Remove(workday);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<Workday> GetWorkdayByIdAsync(string id)
        {
            Workday workdayFound = null;
            workdayFound = await _context.Workdays.Where(w => w.Id == id).FirstOrDefaultAsync();
            return workdayFound;
        }
    }
}
