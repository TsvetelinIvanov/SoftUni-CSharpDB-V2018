using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _06AddingANewAddressAndUpdatingEmployee
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                Address address = new Address() { AddressText = "Vitoshka 15", TownId = 4 };
                foreach (Employee employee in softUniContext.Employees.Where(e => e.LastName == "Nakov"))
                {
                    employee.Address = address;
                }

                softUniContext.SaveChanges();
                
                IQueryable<string> tenAddressTextsOrderedByAddressId = softUniContext.Employees
                    .OrderByDescending(e => e.AddressId).Take(10).Select(e => e.Address.AddressText);
                foreach (string adressText in tenAddressTextsOrderedByAddressId)
                {
                    Console.WriteLine(adressText);
                }
            }
        }
    }
}
