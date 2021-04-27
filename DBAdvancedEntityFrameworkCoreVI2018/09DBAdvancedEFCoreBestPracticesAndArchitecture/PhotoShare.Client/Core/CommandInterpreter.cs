using System;
using System.Linq;
using System.Reflection;
using PhotoShare.Client.Core.Contracts;

namespace PhotoShare.Client.Core
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
            string inputCommand = input[0] + "Command";

            string[] args = input.Skip(1).ToArray();

            Type type = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(c => c.Name == inputCommand);
            if (type == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            ConstructorInfo constructor = type.GetConstructors().First();

            Type[] constructorParameters = constructor.GetParameters().Select(x => x.ParameterType).ToArray();

            object[] service = constructorParameters.Select(serviceProvider.GetService).ToArray();

            ICommand command = (ICommand)constructor.Invoke(service);

            string result = command.Execute(args);

            return result;
        }
    }
}