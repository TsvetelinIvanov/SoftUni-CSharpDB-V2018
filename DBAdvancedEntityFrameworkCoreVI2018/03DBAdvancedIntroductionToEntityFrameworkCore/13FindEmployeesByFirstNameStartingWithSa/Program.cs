using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _13FindEmployeesByFirstNameStartingWithSa
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                IOrderedQueryable<Employee> employeesWithFirstNameStartingWithSa = softUniContext.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))                    
                    .OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
                foreach (Employee employee in employeesWithFirstNameStartingWithSa)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
                }
            }
        }
    }
}