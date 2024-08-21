using Newtonsoft.Json;
using ProductShop.Data;
using System.IO;
using System.Linq;

namespace ProductShop.App
{
    public class ExportSerializer
    {
        private readonly ProductShopContext context;

        public ExportSerializer(ProductShopContext context)
        {
            this.context = context;
        }

        public void ExportProductsInRange()
        {
            var products = this.context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName ?? p.Seller.LastName
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText("ExportJson/products-in-range.json", jsonProductsString);
        }

        public void ExportSccessfullySoldProducts()
        {
            var users = this.context.Users
                .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Where(ps => ps.Buyer != null)
                                .Select(ps => new
                                {
                                    name = ps.Name,
                                    price = ps.Price,
                                    buyerFirstName = ps.Buyer.FirstName,
                                    buyerLastName = ps.Buyer.LastName
                                })
                                .ToArray()
                })
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("ExportJson/users-sold-products.json", jsonProductsString);
        }

        public void ExportCategoriesByProductsCount()
        {
            //var categories = this.context.Categories
            //    .OrderByDescending(c => c.CategoryProducts.Count)
            //    .Select(c => new
            //    {
            //        category = c.Name, 
            //        productsCount = c.CategoryProducts.Count,
            //        //averagePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
            //        averagePrice = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Average(),
            //        totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
            //    })
            //    .ToArray();

            var categories = this.context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    //averagePrice = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Average(),
                    averagePrice = c.CategoryProducts.Sum(cp => cp.Product.Price) / c.CategoryProducts.Count,
                    totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.productsCount)
                .ToArray();

            string jsonProductsString = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("ExportJson/categories-by-products.json", jsonProductsString);
        }

        public void ExportUsersAndProducts()
        {
            var users = new
            {
                usersCount = this.context.Users.Count(),
                users = this.context.Users
                .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count,
                        products = u.ProductsSold.Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price
                        })
                        .ToArray()
                    }
                })
                .ToArray()
            };

            string jsonProductsString = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("ExportJson/users-and-products.json", jsonProductsString);
        }

        //public void ExportUsersAndProducts()
        //{
        //    var users = this.context.Users
        //    .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(ps => ps.Buyer != null))
        //    .OrderByDescending(u => u.ProductsSold.Count)
        //    .ThenBy(u => u.LastName)
        //    .Select(u => new
        //    {
        //        firstName = u.FirstName,
        //        lastName = u.LastName,
        //        age = u.Age,
        //        soldProducts = new
        //        {
        //            count = u.ProductsSold.Count,
        //            products = u.ProductsSold.Select(ps => new
        //            {
        //                name = ps.Name,
        //                price = ps.Price
        //            })
        //        }
        //    })
        //    .ToArray();

        //    var user = new
        //    {
        //        usersCount = users.Count(),
        //        users = users
        //    };

        //    string jsonProductsString = JsonConvert.SerializeObject(user, new JsonSerializerSettings
        //    {
        //        Formatting = Formatting.Indented,
        //        NullValueHandling = NullValueHandling.Ignore
        //    });
        //    File.WriteAllText("ExportJson/users-and-products.json", jsonProductsString);
        //}       
    }
}
