using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using System;

namespace Employees.App.Core.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IEmployeeController employeeController;

        public EmployeeInfoCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(InvalidCommandArgumentExceptionMessage);
            }

            int id = int.Parse(args[0]);
            EmployeeDto emloyeeDto = this.employeeController.GetEmployeeInfo(id);

            string employeeInfoString = $"ID: {emloyeeDto.Id} - {emloyeeDto.FirstName} {emloyeeDto.LastName} - ${emloyeeDto.Salary:f2}";

            return employeeInfoString;
        }
    }
}