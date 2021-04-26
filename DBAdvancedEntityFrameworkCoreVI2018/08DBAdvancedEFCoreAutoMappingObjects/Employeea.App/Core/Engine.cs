using Employees.App.Core.Contracts;
using Employees.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Employees.App.Core
{
    public class Engine : IEngine
    {
        private const string EnterCommandMessage = "Enter command: ";

        private readonly IServiceProvider serviceProvider;

        private IReader reader;
        private IWriter writer;        

        public Engine(IServiceProvider serviceProvider, IReader reader, IWriter writer) 
        {
            this.serviceProvider = serviceProvider;
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            IDbInitializerService initializeDb = this.serviceProvider.GetService<IDbInitializerService>();
            initializeDb.InitializeDatabase();

            ICommandInterpreter commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();
            while (true)
            {
                this.writer.Write(EnterCommandMessage);
                string[] input = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    string result = commandInterpreter.Read(input);
                    writer.WriteLine(result);
                }
                catch (ArgumentException ae)
                {
                    this.writer.WriteLine(ae.Message);
                }
            }
        }
    }
}