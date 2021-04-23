using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _08AddressesByTown
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                var addresses = softUniContext.Addresses.OrderByDescending(a => a.Employees.Count)
                    .ThenBy(a => a.Town.Name).ThenBy(a => a.AddressText).Take(10)
                    .Select(a => new
                    {
                        a.AddressText,
                        a.Town.Name,
                        EmployeesCount = a.Employees.Count
                    });

                foreach (var address in addresses)
                {
                    Console.WriteLine($"{address.AddressText}, {address.Name} - {address.EmployeesCount} employees");
                }
            }
        }
    }
}