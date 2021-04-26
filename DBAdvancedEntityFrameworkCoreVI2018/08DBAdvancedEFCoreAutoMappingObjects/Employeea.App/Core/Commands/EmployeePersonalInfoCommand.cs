using Employees.App.Core.Contracts;
using Employees.App.Core.DTOs;
using System;

namespace Employees.App.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IEmployeeController employeeController;

        public EmployeePersonalInfoCommand(IEmployeeController employeeController)
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
            EmployeePersonalInfoDto employeePersonalInfoDto =  this.employeeController.GetEmployeePersonalInfo(id);

            string employeePersonalInfo = $"ID: {employeePersonalInfoDto.Id} - {employeePersonalInfoDto.FirstName} {employeePersonalInfoDto.LastName} - ${employeePersonalInfoDto.Salary:f2}{Environment.NewLine}" +
                $"Birthday: {employeePersonalInfoDto.Birthday.Value.ToString("dd-MM-yyyy")}{Environment.NewLine}" +
                $"Address: {employeePersonalInfoDto.Address}";

            return employeePersonalInfo;
        }
    }
}