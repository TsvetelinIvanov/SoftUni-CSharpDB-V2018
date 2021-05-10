using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.ExportDTOs;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CarDealer
{
    public class ExportSerializer 
    {
        private readonly CarDealerContext context;

        public ExportSerializer(CarDealerContext context)
        {
            this.context = context;
        }

        public void ExportCarsWithDistance()
        {
            Car[] carsWithDistance = this.context.Cars.Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make).ThenBy(c => c.Model).ToArray();
            CarWithDistanceDto[] carWithDistanceDtos = carsWithDistance.AsQueryable().ProjectTo<CarWithDistanceDto>().ToArray();
            StringBuilder carWithDistanceBuilder = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(carWithDistanceDtos.GetType(), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(carWithDistanceBuilder), carWithDistanceDtos, serializerNamespaces);

            File.WriteAllText("DataResults/cars.xml", carWithDistanceBuilder.ToString());
        }

        public void ExportCarsFromMakeFerarri()
        {
            Car[] carsFromFerrari = this.context.Cars.Where(c => c.Make == "Ferrari")
                .OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance).ToArray();
            CarFromFerrariDto[] carFromFerrariDtos = carsFromFerrari.AsQueryable().ProjectTo<CarFromFerrariDto>().ToArray();
            StringBuilder carsFromFerrariBuilder = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(carFromFerrariDtos.GetType(), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(carsFromFerrariBuilder), carFromFerrariDtos, serializerNamespaces);

            File.WriteAllText("DataResults/ferrari-cars.xml", carsFromFerrariBuilder.ToString());
        }

        public void ExportLocalSuppliers()
        {
            Supplier[] localSuppliers = this.context.Suppliers
                .Include(s => s.Parts)
                .Where(s => s.IsImporter == false)
                .ToArray();
            LocalSupplierDto[] localSupplierDtos = localSuppliers.AsQueryable().ProjectTo<LocalSupplierDto>().ToArray();
            StringBuilder localSuppliersBuilder = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(localSupplierDtos.GetType(), new XmlRootAttribute("suppliers"));
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(localSuppliersBuilder), localSupplierDtos, serializerNamespaces);

            File.WriteAllText("DataResults/local-suppliers.xml", localSuppliersBuilder.ToString());
        }

        public void ExportCarsWithThierListOfParts()
        {
            Car[] cars = this.context.Cars
                .Include(c => c.PartCars)
                .ThenInclude(pc => pc.Part)
                .ToArray();
            ExportCarDto[] exportCarDtos = cars.AsQueryable().ProjectTo<ExportCarDto>().ToArray();
            StringBuilder exportCarBuilder = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(exportCarDtos.GetType(), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(exportCarBuilder), exportCarDtos, serializerNamespaces);

            File.WriteAllText("DataResults/cars-and-parts.xml", exportCarBuilder.ToString());
        }

        public void ExportTotalSalesByCustomer()
        {
            Customer[] customers = this.context.Customers
                .Include(c => c.Sales)
                    .ThenInclude(s => s.Car)
                    .ThenInclude(c => c.PartCars)
                    .ThenInclude(pc => pc.Part)
                .Where(c => c.Sales.Count >= 1)
                .ToArray();

            ExportCustomerDto[] exportCustomerDtos = customers.Select(c => new ExportCustomerDto
            {
                FullName = c.Name,
                BoughtCars = c.Sales.Count,
                SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price) * (1 - s.Discount - (c.IsYoungDriver ? 0.05m : 0m)))
            })
            .OrderByDescending(c => c.SpentMoney)
            .ThenByDescending(c => c.BoughtCars)
            .ToArray();

            StringBuilder totalSalesBuilder = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(exportCustomerDtos.GetType(), new XmlRootAttribute("customers"));
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(totalSalesBuilder), exportCustomerDtos, serializerNamespaces);

            File.WriteAllText("DataResults/customers-total-sales.xml", totalSalesBuilder.ToString());
        }        

        public void ExportSalesWithAppliedDiscount()
        {
            Sale[] sales = this.context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Car)
                    .ThenInclude(c => c.PartCars)
                    .ThenInclude(pc => pc.Part)
                .ToArray();

            ExportSaleDto[] exportSaleDtos = new ExportSaleDto[sales.Length];
            for (int i = 0; i < sales.Length; i++)
            {
                Sale sale = sales[i];
                ExportSaleDto exportSaleDto = Mapper.Map<ExportSaleDto>(sale);
                exportSaleDto.Price = sale.Car.PartCars.Sum(pc => pc.Part.Price);
                exportSaleDto.PriceWithDiscount = sale.Car.PartCars.Sum(pc => pc.Part.Price) * (1 - sale.Discount - (sale.Customer.IsYoungDriver ? 0.05m : 0m));
                exportSaleDtos[i] = exportSaleDto;
            }

            StringBuilder salesBuilder = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(exportSaleDtos.GetType(), new XmlRootAttribute("sales"));
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(salesBuilder), exportSaleDtos, serializerNamespaces);

            File.WriteAllText("DataResults/sales-discounts.xml", salesBuilder.ToString());
        }
    }
}