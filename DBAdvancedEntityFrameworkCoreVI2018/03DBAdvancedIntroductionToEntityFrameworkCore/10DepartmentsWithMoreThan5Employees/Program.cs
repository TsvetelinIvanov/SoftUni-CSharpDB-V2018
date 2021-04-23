using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _10DepartmentsWithMoreThan5Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                var bigDepartments = softUniContext.Departments.Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count).ThenBy(d => d.Name)
                    .Select(d => new
                    {
                        d.DepartmentId,
                        ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                        d.Name
                    });

                foreach (var department in bigDepartments)
                {                    
                    Console.WriteLine($"{department.Name} - {department.ManagerName}");
                    IOrderedQueryable<Employee> employeesFromDedartment = softUniContext.Employees
                        .Where(e => e.DepartmentId == department.DepartmentId)
                        .OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
                    foreach (Employee employee in employeesFromDedartment)
                    {
                        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                    }
                    
                    Console.WriteLine("----------");
                }
            }
        }
    }
}