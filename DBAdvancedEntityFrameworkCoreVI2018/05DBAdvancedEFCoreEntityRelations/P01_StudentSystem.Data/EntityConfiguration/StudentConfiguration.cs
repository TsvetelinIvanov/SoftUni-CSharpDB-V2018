using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.Name).HasMaxLength(100).IsUnicode().IsRequired();

            builder.Property(s => s.PhoneNumber).HasColumnType("CHAR(10)").IsUnicode(false).IsRequired(false);

            builder.Property(s => s.RegisteredOn).IsRequired();

            builder.Property(s => s.Birthday).IsRequired(false);
        }
    }
}