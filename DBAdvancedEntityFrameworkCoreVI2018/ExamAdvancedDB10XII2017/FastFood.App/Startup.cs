using System;
using System.IO;
using AutoMapper;
using FastFood.Data;
using FastFood.DataProcessor;

namespace FastFood.App
{
    public class Startup
    {
	public static void Main(string[] args)
	{
	   FastFoodDbContext context = new FastFoodDbContext();

	   ResetDatabase(context);
	   Console.WriteLine("Database Reset.");

            Mapper.Initialize(cfg => cfg.AddProfile<FastFoodProfile>());

            ImportEntities(context);
            ExportEntities(context);
            BonusTask(context);
        }

        private static void ResetDatabase(FastFoodDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void ImportEntities(FastFoodDbContext context, string baseDirectory = @"..\Datasets\")
        {
            //baseDirectory = @"Datasets\";
            //baseDirectory = @"FastFood.App\Datasets\";
            //baseDirectory = @"ExamAdvancedDB10XII2017\FastFood.App\Datasets\";
            //baseDirectory = @"[Absolute Local Path]";

            const string exportDirectory = "./Results/";
            //const string exportDirectory = @"Results\";
            //const string exportDirectory = @"FastFood.App\Results\";
            //const string exportDirectory = @"ExamAdvancedDB10XII2017\FastFood.App\Results\";
            //const string exportDirectory = @"[Absolute Local Path]";            

            string employees = Deserializer.ImportEmployees(context, File.ReadAllText(baseDirectory + "employees.json"));
            PrintAndExportEntityToFile(employees, exportDirectory + "Employees.txt");

            string items = Deserializer.ImportItems(context, File.ReadAllText(baseDirectory + "items.json"));
            PrintAndExportEntityToFile(items, exportDirectory + "Items.txt");

            string orders = Deserializer.ImportOrders(context, File.ReadAllText(baseDirectory + "orders.xml"));
            PrintAndExportEntityToFile(orders, exportDirectory + "Orders.txt");
        }

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }

        private static void ExportEntities(FastFoodDbContext context)
	{
            const string exportDirectory = "./Results/";
            //const string exportDirectory = @"Results\";
            //const string exportDirectory = @"FastFood.App\Results\";
            //const string exportDirectory = @"ExamAdvancedDB10XII2017\FastFood.App\Results\";
            //const string exportDirectory = @"[Absolute Local Path]";

            string jsonOutput = Serializer.ExportOrdersByEmployee(context, "Avery Rush", "ToGo");
	    Console.WriteLine(jsonOutput);
	    File.WriteAllText(exportDirectory + "OrdersByEmployee.json", jsonOutput);

	    string xmlOutput = Serializer.ExportCategoryStatistics(context, "Chicken,Drinks,Toys");
	    Console.WriteLine(xmlOutput);
	    File.WriteAllText(exportDirectory + "CategoryStatistics.xml", xmlOutput);
	}

	private static void BonusTask(FastFoodDbContext context)
	{
	    string bonusOutput = Bonus.UpdatePrice(context, "Cheeseburger", 6.50m);
	    Console.WriteLine(bonusOutput);
	}				
    }
}
