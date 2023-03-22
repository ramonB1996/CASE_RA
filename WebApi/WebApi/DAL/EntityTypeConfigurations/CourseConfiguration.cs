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
        }
    }
}

