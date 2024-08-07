﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.Name).HasMaxLength(80).IsUnicode().IsRequired();

            builder.Property(c => c.Description).IsUnicode().IsRequired(false);

            builder.Property(c => c.StartDate).IsRequired();

            builder.Property(c => c.EndDate).IsRequired();

            builder.Property(c => c.Price).IsRequired();
        }
    }
}