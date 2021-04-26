using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using System;
using System.Text;

namespace Employees.App.Core.Commands
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IEmployeeController employeeController;

        public ListEmployeesOlderThanCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(InvalidCommandArgumentExceptionMessage);
            }

            int age = int.Parse(args[0]);

            EmployeeOfficeInfoDto[]  employeeOfficeInfoDtos = this.employeeController.GetListedEmployeesOlderThan(age);

            StringBuilder ListedEmployeesBuilder = new StringBuilder();             
            foreach (EmployeeOfficeInfoDto employeeOfficeInfoDto in employeeOfficeInfoDtos)
            {
                string manager = employeeOfficeInfoDto.Manager == null ? "[no manager]" : employeeOfficeInfoDto.Manager.LastName;

                ListedEmployeesBuilder.AppendLine($"{employeeOfficeInfoDto.FirstName} {employeeOfficeInfoDto.LastName} - ${employeeOfficeInfoDto.Salary:f2} - Manager: {manager}");
            }

            return ListedEmployeesBuilder.ToString().TrimEnd();
        }
    }
}