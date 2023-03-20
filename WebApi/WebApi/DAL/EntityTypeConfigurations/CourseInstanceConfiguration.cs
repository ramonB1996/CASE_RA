using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain;
using WebApi.DAL.Comparers;
using WebApi.DAL.Converters;

namespace WebApi.DAL.EntityTypeConfigurations
{
    public class CourseInstanceConfiguration : IEntityTypeConfiguration<CourseInstance>
    {
        public void Configure(EntityTypeBuilder<CourseInstance> builder)
        {
            builder
                .ToTable("CourseInstances");

            builder
                .Property(p => p.StartDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            // Default Data
            builder.HasData(
                    new CourseInstance() { Id = 1, CourseId = 1, StartDate = new DateOnly(2018, 10, 8) },
                    new CourseInstance() { Id = 2, CourseId = 2, StartDate = new DateOnly(2018, 8, 26) },
                    new CourseInstance() { Id = 3, CourseId = 3, StartDate = new DateOnly(2018, 10, 8) },
                    new CourseInstance() { Id = 4, CourseId = 4, StartDate = new DateOnly(2018, 10, 10) },
                    new CourseInstance() { Id = 5, CourseId = 5, StartDate = new DateOnly(2018, 8, 31) },
                    new CourseInstance() { Id = 6, CourseId = 6, StartDate = new DateOnly(2018, 9, 8) });
        }
    }
}

