using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _12IncreaseSalaries
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                IQueryable<Employee> employeesForIncreasingSalaries = softUniContext.Employees
                    .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services");
                foreach (Employee employee in employeesForIncreasingSalaries)
                {
                    employee.Salary *= 1.12m;
                }

                softUniContext.SaveChanges();

                IOrderedQueryable<Employee> employeesWithIncreasedSalaries = softUniContext.Employees
                    .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
                    .OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
                foreach (Employee employee in employeesWithIncreasedSalaries)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
                }
            }
        }
    }
}
