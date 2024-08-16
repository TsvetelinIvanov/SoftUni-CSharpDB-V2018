using AutoMapper;
using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using Employees.Data;
using Employees.Models;
using System;
using System.Linq;

namespace Employees.App.Core.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        private const string InvalidEmployeeIdExceptionMessage = "Employee with this id: \"{0}\", doesn't exist in database!";

        private readonly EmployeesContext context;
        private readonly IMapper mapper;

        public EmployeeController(EmployeesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = this.mapper.Map<Employee>(employeeDto);

            this.context.Employees.Add(employee);

            this.context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            Employee employee = this.context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException(string.Format(InvalidEmployeeIdExceptionMessage, employeeId));
            }

            employee.Address = address;
            this.context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            Employee employee = this.context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException(string.Format(InvalidEmployeeIdExceptionMessage, employeeId));
            }

            employee.Birthday = date;
            this.context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            //EmployeeDto employeeDto = context.Employees.Where(e => e.Id == employeeId)
            //    .ProjectTo<EmployeeDto>()
            //    .SingleOrDefault();

            Employee employee = this.context.Employees.Find(employeeId);
            EmployeeDto employeeDto = this.mapper.Map<EmployeeDto>(employee);
            if (employeeDto == null)
            {
                throw new ArgumentException(string.Format(InvalidEmployeeIdExceptionMessage, employeeId));
            }

            return employeeDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            //EmployeePersonalInfoDto employeePersonalInfoDto = context.Employees.Where(e => e.Id == employeeId)
            //    .ProjectTo<EmployeePersonalInfoDto>()
            //    .SingleOrDefault();

            Employee employee = this.context.Employees.Find(employeeId);
            EmployeePersonalInfoDto employeePersonalInfoDto = this.mapper.Map<EmployeePersonalInfoDto>(employee);
            if (employeePersonalInfoDto == null)
            {
                throw new ArgumentException(string.Format(InvalidEmployeeIdExceptionMessage, employeeId));
            }

            return employeePersonalInfoDto;
        }
        
        public EmployeeOfficeInfoDto[] GetListedEmployeesOlderThan(int age)
        {
            //EmployeeOfficeInfoDto[] employees = context.Employees.Where(e => e.Birthday != null)
            //     .Select(e => Mapper.Map<EmployeeOfficeInfoDto>(e))
            //     .Where(e => e.Age > age)
            //     .OrderByDescending(e => e.Salary)
            //     .ToArray();

            Employee[] employees = this.context.Employees.Where(e => e.Birthday != null).ToArray();
            EmployeeOfficeInfoDto[] employeeOfficeInfoDtos = new EmployeeOfficeInfoDto[employees.Length];
            for (int i = 0; i < employees.Length; i++)
            {
                employeeOfficeInfoDtos[i] = this.mapper.Map<EmployeeOfficeInfoDto>(employees[i]);
            }

            employeeOfficeInfoDtos = employeeOfficeInfoDtos.Where(e => e.Age > age)
                .OrderByDescending(e => e.Salary)
                .ToArray();

            return employeeOfficeInfoDtos;
        }
    }
}
