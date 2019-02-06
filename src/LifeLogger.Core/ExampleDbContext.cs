using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeLogger.Core.Example;
using Microsoft.EntityFrameworkCore;

namespace LifeLogger.Core
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
