using Employees.Models;
using System;

namespace Employees.App.Core.DTOs
{
    public class EmployeeOfficeInfoDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }       

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public int Age => (int)Math.Ceiling((DateTime.Now - this.Birthday.Value).TotalDays / 265.2422);
    }
}