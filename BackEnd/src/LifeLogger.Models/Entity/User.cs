using Microsoft.AspNetCore.Identity;
using System;

namespace LifeLogger.Models.Entity
{
    public class User : IdentityUser
    {
        // Inherits:
        // public long Id { get; set; }
        // public string UserName { get; set; }
        // public string Email { get; set; }
        // public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public byte[] Salt { get; set; }
    }
}
