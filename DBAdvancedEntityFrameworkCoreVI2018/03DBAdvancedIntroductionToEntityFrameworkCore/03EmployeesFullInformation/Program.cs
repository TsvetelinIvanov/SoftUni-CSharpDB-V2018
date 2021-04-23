using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _03EmployeesFullInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniDb = new SoftUniContext();
            using (softUniDb)
            {
                var employees = softUniDb.Employees
                    .OrderBy(e => e.EmployeeId)
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        MiddleName = e.MiddleName,
                        JobTitle = e.JobTitle,
                        Salary = e.Salary
                    }).ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
                }
            }
        }
    }
}