using Employees.App.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Employees.App.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] input)
        {
            string commandName = input[0] + "Command";
            string[] args = input.Skip(1).ToArray();

            Type Type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(t => t.Name == commandName);
            if (Type == null)
            {
                throw new ArgumentException("Invalid command!");
            }

            ConstructorInfo constructor = Type.GetConstructors().First();
            Type[] constructorParameters = constructor.GetParameters().Select(c => c.ParameterType).ToArray();
            object[] service = constructorParameters.Select(serviceProvider.GetService).ToArray();

            ICommand command = (ICommand)constructor.Invoke(service);
            string result = command.Execute(args);

            return result;
        }
    }
}