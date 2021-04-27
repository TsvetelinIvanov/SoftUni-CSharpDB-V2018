using System;
using PhotoShare.Client.Core.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] data)
        {
            Console.WriteLine("Good Bye!");

            Environment.Exit(0);

            return "Good Bye!";
        }
    }
}