using P02_DatabaseFirst.Data.Models;
using System;
using System.Globalization;
using System.Linq;

namespace _07EmployeesAndProjects
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                var employeesWithProjectsStarted2001_2013 = softUniContext.Employees
                    .Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2013))
                    .Take(30)
                    .Select(e => new
                    {
                        EmployeeName = e.FirstName + " " + e.LastName,
                        ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                        Projects = e.EmployeesProjects.Select(ep => new
                        {
                            ep.Project.Name,
                            ep.Project.StartDate,
                            ep.Project.EndDate
                        })
                    });

                foreach (var employee in employeesWithProjectsStarted2001_2013)
                {
                    Console.WriteLine($"{employee.EmployeeName} - Manager: {employee.ManagerName}");
                    foreach (var project in employee.Projects)
                    {
                        string startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        string endDate = project.EndDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) ?? "not finished";

                        Console.WriteLine($"--{project.Name} - {startDate} - {endDate}");
                    }
                }                    
            }
        }
    }
}