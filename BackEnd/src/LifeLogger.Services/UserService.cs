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
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByHeaderAsync(string header);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
    public class UserService : IUserService
    {
        private readonly LifeLoggerDbContext _context;
        private readonly IConfiguration _config;
        private readonly IJWTHandler _JWTHandler;

        public UserService(LifeLoggerDbContext context, IConfiguration configuration, IJWTHandler JWTHandler)
        {
            _context = context;
            _config = configuration;
            _JWTHandler = JWTHandler;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task DeleteUserAsync(User user)
        {
            // TODO
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

        public async Task<User> GetUserByHeaderAsync(string header)
        {
            string token = GetTokenFromHeader(header);
            string userId = _JWTHandler.GetUserIdFromToken(token);
            User userFound = await GetUserByIdAsync(userId);
            return userFound;
        }

        public string GetTokenFromHeader(string header)
        {
            // remove 'Bearer '
            return header.Remove(0, 7);
        }
    }
}
