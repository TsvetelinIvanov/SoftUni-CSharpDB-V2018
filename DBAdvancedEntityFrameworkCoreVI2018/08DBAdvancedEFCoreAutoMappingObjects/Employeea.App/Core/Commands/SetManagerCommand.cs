using Employees.App.Core.Contracts;
using System;

namespace Employees.App.Core.Commands
{
    public class SetManagerCommand : ICommand
    {
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";

        private readonly IManagerController managerController;

        public SetManagerCommand(IManagerController managerController)
        {
            this.managerController = managerController;
        }

        public string Execute(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException(InvalidCommandArgumentExceptionMessage);
            }

            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            string reportMessage = this.managerController.SetManager(employeeId, managerId);

            return reportMessage;
        }
    }
}