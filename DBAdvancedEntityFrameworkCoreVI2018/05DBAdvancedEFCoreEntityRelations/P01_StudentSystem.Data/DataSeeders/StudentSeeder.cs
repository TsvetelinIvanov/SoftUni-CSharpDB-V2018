using P01_StudentSystem.Data.Models;
using System;

namespace P01_StudentSystem.Data.DataSeeders
{
    public class StudentSeeder
    {
        public static Student[] SeedStudents(StudentSystemContext context)
        {
            Student[] studentsForSeeding = new Student[]
            {
                new Student(){StudentId = 1, Name = "Gun Xo", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 52)},
                new Student(){StudentId = 2, Name = "Run Jo", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 53)},
                new Student(){StudentId = 3, Name = "Vu Min", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 54)},
                new Student(){StudentId = 4, Name = "Ran Pi", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 55)},
                new Student(){StudentId = 5, Name = "Tan Su", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 56)},
                new Student(){StudentId = 6, Name = "Mo Joy", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 57)},
                new Student(){StudentId = 7, Name = "Min Xo", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 58)},
                new Student(){StudentId = 8, Name = "Tin Pu", RegisteredOn = new DateTime(2018, 5, 1, 8, 30, 59)},
                new Student(){StudentId = 9, Name = "Han Zin", RegisteredOn = new DateTime(2018, 5, 1, 8, 31, 01)},
            };

            return studentsForSeeding;
        }
    }
}