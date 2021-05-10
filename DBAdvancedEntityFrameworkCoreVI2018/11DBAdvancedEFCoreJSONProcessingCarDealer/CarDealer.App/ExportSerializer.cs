using CarDealer.Data;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace CarDealer.App
{
    public class ExportSerializer
    {
        private readonly CarDealerContext context;

        public ExportSerializer(CarDealerContext context)
        {
            this.context = context;
        }

        public void ExportOrderedCustomers()
        {
            var customers = this.context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenByDescending(c => c.IsYoungDriver.ToString())
                .Select(c => new
                {
                    c.Id, 
                    c.Name,
                    c.BirthDate, 
                    c.IsYoungDriver,
                    //c.Sales
                    Sales = c.Sales.ToArray()
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(customers, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("ExportJson/ordered-customers.json", jsonProductsString);

            Console.WriteLine("\"ordered-customers.json\" was exported.");
        }

        public void ExportCarsFromMakeToyota()
        {
            var cars = this.context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(cars, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("ExportJson/toyota-cars.json", jsonProductsString);

            Console.WriteLine("\"toyota-cars.json\" was exported.");    
        }

        public void ExportLocalSuppliers()
        {
            var suppliers = this.context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            File.WriteAllText("ExportJson/local-suppliers.json", jsonProductsString);

            Console.WriteLine("\"local-suppliers.json\" was exported.");
        }

        public void ExporCarsWithThierListOfParts()
        {
            var cars = this.context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(pc => new
                    {
                        pc.Part.Name,
                        pc.Part.Price
                    }).ToArray()
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("ExportJson/cars-and-parts.json", jsonProductsString);

            Console.WriteLine("\"cars-and-parts.json\" was exported.");
        }

        public void ExportTotalSalesByCustomer()
        {
            var customers = this.context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price)) - (c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price)) * c.Sales.Sum(s => s.Discount) / 100)
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("ExportJson/customers-total-sales.json", jsonProductsString);

            Console.WriteLine("\"customers-total-sales.json\" was exported.");
        }

        public void ExportSalesWithAppliedDiscount()
        {
            var sales = this.context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    s.Discount,
                    price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    priceWithDicount = s.Car.PartCars.Sum(pc => pc.Part.Price) * (1 - s.Discount - (s.Customer.IsYoungDriver ? 0.05m : 0m))                     
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(sales, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("ExportJson/sales-discounts.json", jsonProductsString);

            Console.WriteLine("\"sales-discounts.json\" was exported.");
        }
    }
}