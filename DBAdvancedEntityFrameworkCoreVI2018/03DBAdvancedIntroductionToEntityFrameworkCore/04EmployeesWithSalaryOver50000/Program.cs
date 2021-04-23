using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _04EmployeesWithSalaryOver50000
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                IQueryable<string> employeeNames = softUniContext.Employees.Where(e => e.Salary > 50000)
                    .OrderBy(e => e.FirstName).Select(e => e.FirstName);

                foreach (string name in employeeNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
    }
}