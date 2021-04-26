using Employees.App.Core.Contracts;
using System;
using System.Linq;

namespace Employees.App.Core.Commands
{
    public class SetAddressCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IEmployeeController employeeController;

        public SetAddressCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException(InvalidCommandArgumentExceptionMessage);
            }

            int id = int.Parse(args[0]);
            string address = string.Join(" ", args.Skip(1)); ;

            this.employeeController.SetAddress(id, address);

            return $"The address \"{address}\" was added successfully!";
        }
    }
}