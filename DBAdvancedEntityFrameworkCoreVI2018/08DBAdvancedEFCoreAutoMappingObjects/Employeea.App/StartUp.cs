using Employees.App.Core;
using Employees.App.Core.Contracts;
using Employees.App.Core.Controllers;
using Employees.App.Core.IO;
using Employees.Data;
using Employees.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;
using Employees.Services;

namespace Employees.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IServiceProvider service = ConfigureSerice();

            Engine engine = new Engine(service, reader, writer);
            engine.Run();
        }

        private static IServiceProvider ConfigureSerice()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<EmployeeProfile>());

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>(); 

            serviceCollection.AddTransient<IEmployeeController, EmployeeController>();

            serviceCollection.AddTransient<IManagerController, ManagerController>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}