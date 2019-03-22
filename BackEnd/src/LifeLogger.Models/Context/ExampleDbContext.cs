using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using LifeLogger.Models.Entity;

namespace LifeLogger.Models.Context
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExampleEntity> ExampleEntities { get; set; }
    }
}
