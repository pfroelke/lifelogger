using Microsoft.EntityFrameworkCore;

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
        public DbSet<Workday> Workdays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(s => s.Workdays).WithOne(s => s.Owner); //.WithOne(s => s.User)
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // configures one-to-many relationship
        //    modelBuilder.Entity<Student>()
        //        .HasRequired<Grade>(s => s.CurrentGrade)
        //        .WithMany(g => g.Students)
        //        .HasForeignKey<int>(s => s.CurrentGradeId);
        //}
    }
}

