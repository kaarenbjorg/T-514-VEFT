using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseAPI.Entities;

// hello
namespace CourseAPI.Services
{
    public class AppDataContext : DbContext
    {
        public DbSet<Course> Courses {get; set;}
        public DbSet<CourseTemplate> CourseTemplates { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<WaitingList> WaitingList { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
