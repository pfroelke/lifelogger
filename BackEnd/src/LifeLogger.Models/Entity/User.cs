using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LifeLogger.Models.Entity
{
    public class User : IdentityUser
    {
        // Inherits:
        // public string Id { get; set; }
        // public string UserName { get; set; }
        // public string Email { get; set; }
        // public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public byte[] Salt { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public int IncomePerHour { get; set; }
        public ICollection<Workday> Workdays { get; set; }
    }
}
