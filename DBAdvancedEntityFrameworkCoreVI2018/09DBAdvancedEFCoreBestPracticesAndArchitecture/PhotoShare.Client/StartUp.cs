﻿using System;
using System.IO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoShare.Data;
using PhotoShare.Client.Core;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client
{
    public class StartUp
    {
	public static void Main()
	{
	    IServiceProvider service = ConfigureServices();
	    Engine engine = new Engine(service);
	    engine.Run();
	}

	private static IServiceProvider ConfigureServices()
	{
	    IServiceCollection serviceCollection = new ServiceCollection();

	    IConfigurationRoot configuration = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json")
		.Build();

	    serviceCollection.AddDbContext<PhotoShareContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

	    serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<PhotoShareProfile>());

	    serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();
	    serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();

	    serviceCollection.AddTransient<IAlbumRoleService, AlbumRoleService>();
	    serviceCollection.AddTransient<IPictureService, PictureService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ITagService, TagService>();
            serviceCollection.AddTransient<IAlbumService, AlbumService>();
            serviceCollection.AddTransient<IAlbumTagService, AlbumTagService>();
            serviceCollection.AddTransient<ITownService, TownService>();

            serviceCollection.AddSingleton<IUserSessionService,UserSessionService>();

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

	    return serviceProvider;
	}
    }
}
