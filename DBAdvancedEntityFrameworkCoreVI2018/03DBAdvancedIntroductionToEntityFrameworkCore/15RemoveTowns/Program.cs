using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace _15RemoveTowns
{
    class Program
    {
        static void Main(string[] args)
        {
            string townNameToRemove = Console.ReadLine();

            SoftUniContext softUniContext = new SoftUniContext();
            using (softUniContext)
            {
                Town townToRemove = softUniContext.Towns.FirstOrDefault(t => t.Name == townNameToRemove);
                if (townToRemove == null)
                {
                    return;
                }

                IQueryable<Address> addressesToRemove = softUniContext.Addresses.Where(a => a.Town == townToRemove);
                int addressesToRemoveCount = addressesToRemove.Count();
                foreach (Employee employee in softUniContext.Employees.Where(e => addressesToRemove.Any(a => a == e.Address)))
                {
                    employee.AddressId = null;
                }

                softUniContext.Addresses.RemoveRange(addressesToRemove);
                softUniContext.Towns.Remove(townToRemove);
                softUniContext.SaveChanges();

                if (addressesToRemoveCount == 1)
                {
                    Console.WriteLine($"{addressesToRemoveCount} address in {townNameToRemove} was deleted");
                }
                else
                {
                    Console.WriteLine($"{addressesToRemoveCount} addresses in {townNameToRemove} were deleted");
                }
            }
        }
    }
}