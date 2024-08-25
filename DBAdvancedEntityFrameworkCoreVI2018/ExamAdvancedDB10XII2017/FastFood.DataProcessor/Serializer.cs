using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
	public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
	{
            OrderType orderTypeEnum = Enum.Parse<OrderType>(orderType);            

            var employee = context.Employees.ToArray().Where(e => e.Name == employeeName)
                .Select(e => new
                {
                    e.Name,
                    Orders = e.Orders.Where(o => o.Type == orderTypeEnum)
                    .Select(o => new
                    {
                        o.Customer,
                        Items = o.OrderItems
                        .Select(oi => new
                        {
                            oi.Item.Name,
                            oi.Item.Price,
                            oi.Quantity
                        })
                        .ToArray(),
                        TotalPrice = o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity)
                    })
                    .OrderByDescending(x => x.TotalPrice)
                    .ThenByDescending(x => x.Items.Length)
                    .ToArray(),
                    TotalMade = e.Orders.Where(o => o.Type == orderTypeEnum)
                    .Sum(o => o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity))
                })
                .FirstOrDefault();
            
            string jsonString = JsonConvert.SerializeObject(employee, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
	}

	public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
	{
            string[] categoriesNames = categoriesString.Split(',');
            CategoryDto[] categoryDtos = context.Categories.Where(c => categoriesNames.Any(cn => cn == c.Name))
                .Select(c => new CategoryDto
                {
                    Name = c.Name,
                    MostPopularItem = c.Items.Select(i => new MostPopularItemDto
                    {
                        Name = i.Name,
                        TotalMade = i.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity),
                        TimesSold = i.OrderItems.Sum(oi => oi.Quantity)
                    })
                    .OrderByDescending(mpi => mpi.TotalMade)
                    .ThenByDescending(mpi => mpi.TimesSold)
                    .FirstOrDefault()
                })
                .OrderByDescending(cD => cD.MostPopularItem.TotalMade)
                .ThenByDescending(cD => cD.MostPopularItem.TimesSold)
                .ToArray();

            StringBuilder exportCategoryStatisticsBuilder = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));
            xmlSerializer.Serialize(new StringWriter(exportCategoryStatisticsBuilder), categoryDtos, xmlNamespaces);

            return exportCategoryStatisticsBuilder.ToString();
	}
    }
}
