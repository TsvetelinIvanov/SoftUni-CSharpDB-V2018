using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using System;
using System.Text;

namespace Employees.App.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IManagerController managerController;

        public ManagerInfoCommand(IManagerController managerController)
        {
            this.managerController = managerController;
        }

        public string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(InvalidCommandArgumentExceptionMessage);
            }

            int managerId = int.Parse(args[0]);

            ManagerDto managerDto = this.managerController.GetManagerInfo(managerId);

            StringBuilder managerInfoBuilder = new StringBuilder();
            managerInfoBuilder.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeesCount}");

            foreach (EmployeeDto employeeDto in managerDto.EmployeeDtos)
            {
                managerInfoBuilder.AppendLine($"    - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
            }

            return managerInfoBuilder.ToString().TrimEnd();
        }
    }
}