using P02_DatabaseFirst.Data.Models;
using System;
using System.Globalization;
using System.Linq;

namespace _11FindLatest10Projects
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                IQueryable<Project> latest10Projects = softUniContext.Projects.OrderByDescending(p => p.StartDate).Take(10);
                foreach (Project project in latest10Projects.OrderBy(p => p.Name))
                {
                    Console.WriteLine(project.Name);
                    Console.WriteLine(project.Description);
                    Console.WriteLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}