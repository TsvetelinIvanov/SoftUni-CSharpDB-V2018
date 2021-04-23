using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _05EmployeesFromResearchAndDevelopment
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                var employeesFromRAndD = softUniContext.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName)
                    .Select(e => new { e.FirstName, e.LastName, DepartmentName = e.Department.Name, e.Salary });

                foreach (var employee in employeesFromRAndD)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:f2}");
                }
            }
        }
    }
}