using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeLogger.Core.Entity;

namespace LifeLogger.Core.Context
{
    public class LifeLoggerDbContext : DbContext
    {
        public LifeLoggerDbContext(DbContextOptions<LifeLoggerDbContext> options)
            : base (options)
        {
            
        }

        public DbSet<LoggedItem> LoggedItem { get; set; }
    }
}
