using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain;
using WebApi.DAL.EntityTypeConfigurations;

namespace WebApi.DAL
{
	public class CourseContext : DbContext
	{
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstance> CourseInstances { get; set; }

        public CourseContext()
        {
        }
         
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var config = builder.Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("CourseDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());
            modelBuilder.ApplyConfiguration<CourseInstance>(new CourseInstanceConfiguration());
        }
    }
}

