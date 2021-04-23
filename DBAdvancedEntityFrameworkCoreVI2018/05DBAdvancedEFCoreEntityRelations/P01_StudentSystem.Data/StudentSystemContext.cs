using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.DataSeeders;
using P01_StudentSystem.Data.EntityConfiguration;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());

            modelBuilder.Entity<Student>().HasData(StudentSeeder.SeedStudents(this));
            modelBuilder.Entity<Course>().HasData(CourseSeeder.SeedCourses(this));
            modelBuilder.Entity<Resource>().HasData(ResourceSeeder.SeedResources(this));
            modelBuilder.Entity<Homework>().HasData(HomeworkSeeder.SeedHmeworks(this));
        }
    }
}