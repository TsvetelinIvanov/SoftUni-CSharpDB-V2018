using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.HomeworkId);

            builder.Property(h => h.Content).IsUnicode(false).IsRequired();

            builder.Property(h => h.ContentType).IsRequired();

            builder.Property(h => h.SubmissionTime).IsRequired();

            builder.Property(h => h.StudentId).IsRequired();
            builder.HasOne(h => h.Student).WithMany(s => s.HomeworkSubmissions).HasForeignKey(h => h.StudentId);

            builder.Property(h => h.CourseId).IsRequired();
            builder.HasOne(h => h.Course).WithMany(c => c.HomeworkSubmissions).HasForeignKey(h => h.CourseId);
        }
    }
}