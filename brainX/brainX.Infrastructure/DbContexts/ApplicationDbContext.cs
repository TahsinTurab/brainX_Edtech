using brainX.Infrastructure.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Account> Accounts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Instructor>()
        //        .HasMany(c => c.Courses)
        //        .WithOne(e => e.Instructor);

        //    modelBuilder.Entity<Course>()
        //        .HasMany(c => c.Contents)
        //        .WithOne(e => e.Course);
        //}
    }
}
