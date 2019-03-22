using System;
using System.Collections.Generic;
using System.Text;
using LifeLogger.Models.Entity;

namespace LifeLogger.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static implicit operator RegisterViewModel(User user)
        {
            return new RegisterViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.PasswordHash
            };
        }

        public static implicit operator User(RegisterViewModel vm)
        {
            return new User
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName,
                Email = vm.Email,
                PasswordHash = vm.Password
            };
        }
    }
}
