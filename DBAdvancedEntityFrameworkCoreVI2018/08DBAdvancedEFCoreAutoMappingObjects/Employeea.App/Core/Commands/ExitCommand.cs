using Employees.App.Core.Contracts;
using System;
using System.Threading;

namespace Employees.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            for (int i = 5; i > 0; i--)
            {
                string message = $"The program will be closed after {i} seconds!";
                string endMessage = $"The program will be closed after {i} second!";

                if (i > 1)
                {
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine(endMessage);
                }

                Thread.Sleep(1000);
            }

            Environment.Exit(0);

            return null;
        }
    }
}