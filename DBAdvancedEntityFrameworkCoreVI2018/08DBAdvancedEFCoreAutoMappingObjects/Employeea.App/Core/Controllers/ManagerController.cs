using AutoMapper;
using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using Employees.Data;
using Employees.Models;
using System;

namespace Employees.App.Core.Controllers
{
    public class ManagerController : IManagerController
    {
        private const string InvalidEmployeeIdExceptionMessage = "Employee with this id: \"{0}\", don't exist in the database!";
        private const string InvalidManagerIdExceptionMessage = "Manager with this id: \"{0}\", don't exist in the database!";

        private readonly EmployeesContext context;
        private readonly IMapper mapper;

        public ManagerController(EmployeesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string SetManager(int employeeId, int managerId)
        {
            Employee employee = this.context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException(string.Format(InvalidEmployeeIdExceptionMessage, employeeId));
            }

            Employee manager = this.context.Employees.Find(managerId);
            if (manager == null)
            {
                throw new ArgumentException(string.Format(InvalidManagerIdExceptionMessage, managerId));
            }

            employee.Manager = manager;
            context.SaveChanges();
            
            string reportMessage = $"The manager {manager.FirstName} {manager.LastName} was set to be supervisior to employee {employee.FirstName} {employee.LastName}!";

            return reportMessage;
        }

        public ManagerDto GetManagerInfo(int employeeId)
        {
            //ManagerDto manager = context.Employees.Where(e => e.Id == employeeId)
            //    .ProjectTo<ManagerDto>()
            //    .SingleOrDefault();

            Employee manager = context.Employees.Find(employeeId);
            ManagerDto managerDto = mapper.Map<ManagerDto>(manager);
            if (manager == null)
            {
                throw new ArgumentException(string.Format(InvalidManagerIdExceptionMessage, employeeId));
            }

            return managerDto;
        }        
    }
}