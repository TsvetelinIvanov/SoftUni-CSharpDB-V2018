using AutoMapper;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            IMapper mapper = config.CreateMapper();

            ProductShopContext context = new ProductShopContext();

            ImportDeserializer importDeserializer = new ImportDeserializer(context, mapper);
            importDeserializer.ImportUsers();
            importDeserializer.ImportProducts();
            importDeserializer.ImportCategories();
            importDeserializer.SetCategoryProducts();

            ExportSerializer exportSerializer = new ExportSerializer(context);
            exportSerializer.ExportProductsInRange();
            exportSerializer.ExportSccessfullySoldProducts();
            exportSerializer.ExportCategoriesByProductsCount();
            exportSerializer.ExportUsersAndProducts();
        }
    }
}