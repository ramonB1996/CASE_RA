using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain;

namespace WebApi.DAL.EntityTypeConfigurations
{
	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .ToTable("Courses")
                .HasMany(c => c.CourseInstances)
                .WithOne(c => c.Course);

            builder.Property(p => p.Duration)
                .IsRequired();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(10);

            // Default Data
            builder.HasData(
                new Course() { Id = 1, Duration = 5, Title = "Programming C#", Code = string.Empty},
                new Course() { Id = 2, Duration = 2, Title = "ECMAscript - What's new", Code = string.Empty },
                new Course() { Id = 3, Duration = 5, Title = "Querying SQL Server", Code = string.Empty },
                new Course() { Id = 4, Duration = 2, Title = "Java Persistence API", Code = string.Empty },
                new Course() { Id = 5, Duration = 3, Title = "Building a SPA with VueJS", Code = string.Empty },
                new Course() { Id = 6, Duration = 5, Title = "ASP.NET MVC", Code = string.Empty });
        }
    }
}

