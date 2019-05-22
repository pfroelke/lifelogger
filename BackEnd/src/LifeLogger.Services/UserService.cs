using LifeLogger.Models.Context;
using LifeLogger.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace LifeLogger.Services
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByNameAsync(string name);
    }
    public class UserService : IUserService
    {
        private readonly LifeLoggerDbContext _context;
        private readonly IConfiguration _config;

        public UserService(LifeLoggerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }
        //public async Task<IEnumerable<User>> ListAsync()
        //{
        //}

        public async Task AddUserAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            User userFound = null;
            userFound = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return userFound;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            User userFound = null;
            userFound = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            return userFound;
        }
    }
}
