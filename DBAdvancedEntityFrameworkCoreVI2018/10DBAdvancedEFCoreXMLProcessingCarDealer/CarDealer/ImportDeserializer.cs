using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.ImportDTOs;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
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
            XmlSerializer serialiser = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            SupplierDto[] deserializedSupliers = (SupplierDto[])serialiser.Deserialize(new MemoryStream(File.ReadAllBytes(@"Datasets/suppliers.xml")));

            Supplier[] suppliers = deserializedSupliers.AsQueryable().ProjectTo<Supplier>().ToArray();
            this.context.Suppliers.AddRange(suppliers);
            this.context.SaveChanges();

            Console.WriteLine("The suppliers are imported.");
        }

        public void ImportParts()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));
            PartDto[] deserializedParts = (PartDto[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(@"Datasets/parts.xml")));

            Part[] parts = deserializedParts.AsQueryable().ProjectTo<Part>().ToArray();

            Supplier[] suppliers = this.context.Suppliers.ToArray();
            Random random = new Random();
            foreach (Part part in parts)
            {
                part.Supplier = suppliers[random.Next(1, suppliers.Length)];
            }

            this.context.Parts.AddRange(parts);
            this.context.SaveChanges();

            Console.WriteLine("The parts are imported.");
        }

        public void ImportCars()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
            CarDto[] deserializedCars = (CarDto[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(@"Datasets/cars.xml")));

            Car[] cars = deserializedCars.AsQueryable().ProjectTo<Car>().ToArray();
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

                    PartCar partCar = new PartCar()
                    {                        
                        CarId = carId,
                        PartId = partId
                    };
                    
                    partCars.Add(partCar);
                    partIds.Add(partId);
                }
            }

            //Car[] cars = this.context.Cars.ToArray();
            //Part[] parts = this.context.Parts.ToArray();
            //Random random = new Random();
            //foreach (Car car in cars)
            //{
            //    int count = random.Next();
            //    //car.PartCars = new List<PartCar>();
            //    HashSet<Part> addedParts = new HashSet<Part>();
            //    for (int i = 0; i < count; i++)
            //    {
            //        Part part;
            //        do
            //        {
            //            part = parts[random.Next(parts.Length)];
            //        }
            //        while (addedParts.Contains(part));

            //        addedParts.Add(part);
            //        car.PartCars.Add(new PartCar
            //        {
            //            Part = part
            //        });
            //    }
            //}

            this.context.PartCars.AddRange(partCars);
            this.context.SaveChanges();

            Console.WriteLine("The partCars are imported.");
        }

        public void ImportCustomers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
            CustomerDto[] deserializedCustomers = (CustomerDto[])serializer.Deserialize(new MemoryStream(File.ReadAllBytes(@"Datasets/customers.xml")));

            Customer[] customers = deserializedCustomers.AsQueryable().ProjectTo<Customer>().ToArray();
            this.context.Customers.AddRange(customers);
            this.context.SaveChanges();

            Console.WriteLine("The customers are inported.");
        }

        public void ImportSales()
        {
            Car[] cars = this.context.Cars.ToArray();
            Customer[] customers = this.context.Customers.ToArray();
            decimal[] discounts = new decimal[] { 0m, 0.05m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };

            //int amount = new Random().Next(150, 250);
            //Sale[] sales = new Sale[amount];
            //for (int i = 0; i < amount; i++)
            //{
            //    sales[i] = new Sale
            //    {
            //        Car = cars[new Random().Next(1, cars.Length)],
            //        Customer = customers[new Random().Next(1, customers.Length)],
            //        Discount = discounts[new Random().Next(1, discounts.Length)]
            //    };
            //}

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

                carIds.Add(carId);
                sales.Add(sale);
            }

            this.context.Sales.AddRange(sales);
            this.context.SaveChanges();

            Console.WriteLine("The sales are inported.");
        }
    }
}
