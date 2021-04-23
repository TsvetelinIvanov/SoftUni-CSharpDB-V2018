using P01_StudentSystem.Data.Models;
using System;

namespace P01_StudentSystem.Data.DataSeeders
{
    public class CourseSeeder
    {
        public static Course[] SeedCourses(StudentSystemContext context)
        {
            Course[] CoursesForSeeding = new Course[]
            {
                new Course() {CourseId = 1, Name = "Statistics", StartDate = new DateTime(2018, 6, 1),
                    EndDate = new DateTime(2018, 9, 1), Price = 390m},
                new Course() {CourseId = 2, Name = "Marketing", StartDate = new DateTime(2018, 6, 1),
                    EndDate = new DateTime(2018, 9, 1), Price = 390m},
                new Course() {CourseId = 3, Name = "Economics", StartDate = new DateTime(2018, 6, 1),
                    EndDate = new DateTime(2018, 9, 1), Price = 390m},
                new Course() {CourseId = 4, Name = "Management", StartDate = new DateTime(2018, 6, 1),
                    EndDate = new DateTime(2018, 9, 1), Price = 390m},
            };

            return CoursesForSeeding;
        }
    }
}