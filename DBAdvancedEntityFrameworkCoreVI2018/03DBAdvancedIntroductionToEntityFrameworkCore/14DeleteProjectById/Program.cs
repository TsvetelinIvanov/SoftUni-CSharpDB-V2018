using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _14DeleteProjectById
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                Project projectToRemove = softUniContext.Projects.Find(2);
                softUniContext.EmployeesProjects.RemoveRange(softUniContext.EmployeesProjects.Where(ep => ep.Project == projectToRemove));
                softUniContext.Projects.Remove(projectToRemove);
                softUniContext.SaveChanges();

                foreach (Project project in softUniContext.Projects.Take(10))
                {
                    Console.WriteLine(project.Name);
                }
            }
        }
    }
}