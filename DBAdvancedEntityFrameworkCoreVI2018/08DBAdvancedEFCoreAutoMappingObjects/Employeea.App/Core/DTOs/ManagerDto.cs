using System.Collections.Generic;

namespace Employees.App.Core.DTOs
{
    public class ManagerDto
    {
        public ManagerDto()
        {
            this.EmployeeDtos = new HashSet<EmployeeDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> EmployeeDtos { get; set; }

        public int EmployeesCount => EmployeeDtos.Count;
    }
}