using AutoMapper;
using CarDealer.Data;

namespace CarDealer
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(configuration => configuration.AddProfile(new CarDealerProfile()));

            CarDealerContext context = new CarDealerContext();
            using (context)
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                ImportDeserializer deserializer = new ImportDeserializer(context);
                deserializer.ImportSuppliers();
                deserializer.ImportParts();
                deserializer.ImportCars();
                deserializer.ImportPartCars();
                deserializer.ImportCustomers();
                deserializer.ImportSales();

                ExportSerializer serializer = new ExportSerializer(context);
                serializer.ExportCarsWithDistance();
                serializer.ExportCarsFromMakeFerarri();
                serializer.ExportLocalSuppliers();
                serializer.ExportCarsWithThierListOfParts();
                serializer.ExportTotalSalesByCustomer();
                serializer.ExportSalesWithAppliedDiscount();                
            }
        }
    }
}