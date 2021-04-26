using Employees.App.Core.Contracts;
using System;

namespace Employees.App.Core.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}