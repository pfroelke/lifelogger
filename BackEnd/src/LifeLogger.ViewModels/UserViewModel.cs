using LifeLogger.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLogger.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public int IncomePerHour { get; set; }

        public static implicit operator UserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                CompanyName = user.CompanyName,
                JobTitle = user.JobTitle,
                IncomePerHour = user.IncomePerHour
            };
        }
    }
}
