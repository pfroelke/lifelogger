﻿using Microsoft.EntityFrameworkCore;

using LifeLogger.Models.Entity;

namespace LifeLogger.Models.Context
{
    public class LifeLoggerDbContext : DbContext
    {
        public LifeLoggerDbContext(DbContextOptions<LifeLoggerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
