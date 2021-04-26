using Employees.App.Core.DTOs;
using System;

namespace Employees.App.Core.Contracts
{
    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime date);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);

        EmployeeOfficeInfoDto[] GetListedEmployeesOlderThan(int age);
    }
}