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
        }
    }
}

