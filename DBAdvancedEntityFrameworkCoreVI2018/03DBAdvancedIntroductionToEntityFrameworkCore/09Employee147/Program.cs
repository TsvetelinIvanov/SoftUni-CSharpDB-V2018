using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _09Employee147
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                Employee employee = softUniContext.Employees.Single(e => e.EmployeeId == 147);
                IQueryable<string> projectNames = softUniContext.EmployeesProjects
                    .Where(ep => ep.EmployeeId == employee.EmployeeId)
                    .OrderBy(ep => ep.Project.Name)
                    .Select(ep => ep.Project.Name);

                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                Console.WriteLine(string.Join(Environment.NewLine, projectNames));
            }
        }
    }
}