using System;

namespace LifeLogger.Models.Entity
{
    public class JWT
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
