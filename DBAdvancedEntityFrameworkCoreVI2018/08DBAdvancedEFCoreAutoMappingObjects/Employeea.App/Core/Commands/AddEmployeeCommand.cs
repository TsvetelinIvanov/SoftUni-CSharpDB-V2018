using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using System;

namespace Employees.App.Core.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IEmployeeController employeeController;

        public AddEmployeeCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            if (args.Length != 3)
            {
                throw new AccessViolationException(InvalidCommandArgumentExceptionMessage);
            }

            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            EmployeeDto employeeDto = new EmployeeDto
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.employeeController.AddEmployee(employeeDto);

            return $"Employee {employeeDto.FirstName} {employeeDto.LastName} was added successfully!";
        }
    }
}