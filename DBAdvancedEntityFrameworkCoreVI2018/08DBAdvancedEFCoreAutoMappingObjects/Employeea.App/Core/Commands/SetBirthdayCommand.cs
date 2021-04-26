using Employees.App.Core.Contracts;
using System;
using System.Globalization;

namespace Employees.App.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private const string DateFormat = "dd-MM-yyyy";
        private const string InvalidCommandArgumentExceptionMessage = "The given command's arguments are invalid!";       
        private const string InvalidDateExceptionMessage = "The date must be in format \"" + DateFormat + "\"!";

        private readonly IEmployeeController employeeController;

        public SetBirthdayCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException(InvalidCommandArgumentExceptionMessage);
            }

            int id = int.Parse(args[0]);
            string dateString = args[1];
            //DateTime date = DateTime.ParseExact(args[1], DateFormat, CultureInfo.InvariantCulture);
            DateTime date = this.TryParseDate(dateString);

            this.employeeController.SetBirthday(id, date);

            return $"The birthday date \"{date.ToString(DateFormat)}\" was added successfully!";
        }

        private DateTime TryParseDate(string dateString)
        {
            bool isParsed = DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime date);
            if (isParsed)
            {
                return date;
            }
            else
            {
                throw new ArgumentException(InvalidDateExceptionMessage);
            }
        }
    }
}