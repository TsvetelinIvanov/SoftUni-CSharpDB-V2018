using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace CarDealer.App
{
    public class ImportDeserializer
    {
        private readonly CarDealerContext context;

        public ImportDeserializer(CarDealerContext context)
        {
            this.context = context;
        }

        public void ImportSuppliers()
        {
            string jsonString = File.ReadAllText("ImportJson/suppliers.json");
            Supplier[] deserializedSuppliers = JsonConvert.DeserializeObject<Supplier[]>(jsonString);
            List<Supplier> suppliers = new List<Supplier>();
            foreach (Supplier supplier in deserializedSuppliers)
            {
                if (!this.IsValid(supplier))
                {
                    continue;
                }

                suppliers.Add(supplier);
            }

            this.context.Suppliers.AddRange(suppliers);
            this.context.SaveChanges();

            Console.WriteLine("The suppliers are imported.");
        }

        public void ImportParts()
        {
            string jsonString = File.ReadAllText("ImportJson/parts.json");
            Part[] deserializedParts = JsonConvert.DeserializeObject<Part[]>(jsonString);
            List<Part> parts = new List<Part>();
            Supplier[] suppliers = this.context.Suppliers.ToArray();
            foreach (Part part in deserializedParts)
            {
                if (!this.IsValid(part))
                {
                    continue;
                }

                part.Supplier = suppliers[new Random().Next(1, suppliers.Length)];
                parts.Add(part);
            }

            this.context.Parts.AddRange(parts);
            this.context.SaveChanges();

            Console.WriteLine("The parts are imported.");
        }

        public void ImportCars()
        {
            string jsonString = File.ReadAllText("ImportJson/cars.json");
            Car[] deserializedCars = JsonConvert.DeserializeObject<Car[]>(jsonString);

            List<Car> cars = new List<Car>();
            foreach (Car car in deserializedCars)
            {
                if (!this.IsValid(car))
                {
                    continue;
                }

                cars.Add(car);
            }

            this.context.Cars.AddRange(cars);
            this.context.SaveChanges();

            Console.WriteLine("The cars are imported.");
        }

        public void ImportPartCars()
        {
            Car[] cars = this.context.Cars.ToArray();
            Part[] parts = this.context.Parts.ToArray();
            List<PartCar> partCars = new List<PartCar>();
            for (int carId = 1; carId <= cars.Length; carId++)
            {
                List<int> partIds = new List<int>();
                int partsCount = new Random().Next(11, 21);
                for (int i = 1; i < partsCount; i++)
                {
                    int partId = new Random().Next(1, parts.Length);
                    if (partIds.Contains(partId))
                    {
                        continue;
                    }

                    PartCar partCar = new PartCar
                    {
                        CarId = carId,
                        PartId = partId
                    };

                    partCars.Add(partCar);
                    partIds.Add(partId);
                }
            }

            this.context.PartCars.AddRange(partCars);
            this.context.SaveChanges();

            Console.WriteLine("The partCars are imported.");
        }

        public void ImportCustomers()
        {
            string jsonString = File.ReadAllText("ImportJson/customers.json");
            Customer[] deserializedCustomers = JsonConvert.DeserializeObject<Customer[]>(jsonString);
            List<Customer> customers = new List<Customer>();
            foreach (Customer customer in deserializedCustomers)
            {
                if (!this.IsValid(customer))
                {
                    continue;
                }

                customers.Add(customer);
            }

            this.context.AddRange(customers);
            this.context.SaveChanges();

            Console.WriteLine("The customers are imported.");
        }

        public void ImportSales()
        {
            Car[] cars = this.context.Cars.ToArray();
            Customer[] customers = this.context.Customers.ToArray();
            decimal[] discounts = new decimal[] { 0m, 0.05m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };

            List<Sale> sales = new List<Sale>();
            List<int> carIds = new List<int>();

            int salesTryingsCount = new Random().Next(10, cars.Length / 2);
            for (int i = 1; i < salesTryingsCount; i++)
            {
                int customerId = new Random().Next(1, customers.Length);
                int carId = new Random().Next(1, cars.Length);
                if (carIds.Contains(carId))
                {
                    continue;
                }

                Sale sale = new Sale
                {
                    CarId = carId,
                    CustomerId = customerId,
                    Discount = discounts[new Random().Next(0, discounts.Length)]
                };

                sales.Add(sale);
                carIds.Add(carId);
            }

            this.context.AddRange(sales);
            this.context.SaveChanges();

            Console.WriteLine("The sales are imported.");
        }

        private bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}