using AutoMapper;
using CarDealer.Data;

namespace CarDealer.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<CardealerProfile>();
            });

            IMapper mapper = configuration.CreateMapper();

            CarDealerContext context = new CarDealerContext();

            using (context)
            {
                ImportDeserializer importDeserializer = new ImportDeserializer(context);
                importDeserializer.ImportSuppliers();
                importDeserializer.ImportParts();
                importDeserializer.ImportCars();
                importDeserializer.ImportPartCars();
                importDeserializer.ImportCustomers();
                importDeserializer.ImportSales();

                ExportSerializer exportSerializer = new ExportSerializer(context);
                exportSerializer.ExportOrderedCustomers();
                exportSerializer.ExportCarsFromMakeToyota();
                exportSerializer.ExportLocalSuppliers();
                exportSerializer.ExporCarsWithThierListOfParts();
                exportSerializer.ExportTotalSalesByCustomer();
                exportSerializer.ExportSalesWithAppliedDiscount();
            }
        }
    }
}