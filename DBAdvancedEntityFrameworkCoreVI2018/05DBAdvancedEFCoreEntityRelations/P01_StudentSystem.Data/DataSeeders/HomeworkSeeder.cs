using P01_StudentSystem.Data.Models;
using System;

namespace P01_StudentSystem.Data.DataSeeders
{
    public class HomeworkSeeder
    {
        public static Homework[] SeedHmeworks(StudentSystemContext context)
        {
            Homework[] homeworksForSeeding = new Homework[]
            {
                new Homework() {HomeworkId = 1, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 30, 58), CourseId = 1, StudentId = 1},
                new Homework() {HomeworkId = 2, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 31, 58), CourseId = 1, StudentId = 2},
                new Homework() {HomeworkId = 3, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 32, 58), CourseId = 1, StudentId = 3},
                new Homework() {HomeworkId = 4, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 33, 58), CourseId = 1, StudentId = 4},
                new Homework() {HomeworkId = 5, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 34, 58), CourseId = 1, StudentId = 5},
                new Homework() {HomeworkId = 6, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 35, 58), CourseId = 1, StudentId = 6},
                new Homework() {HomeworkId = 7, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 36, 58), CourseId = 1, StudentId = 7},
                new Homework() {HomeworkId = 8, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 37, 58), CourseId = 1, StudentId = 8},
                new Homework() {HomeworkId = 9, Content = "01Introduction", ContentType = ContentType.Zip,
                SubmissionTime = new DateTime(2018, 6, 1, 11, 38, 58), CourseId = 1, StudentId = 9},
            };

            return homeworksForSeeding;
        }
    }
}
