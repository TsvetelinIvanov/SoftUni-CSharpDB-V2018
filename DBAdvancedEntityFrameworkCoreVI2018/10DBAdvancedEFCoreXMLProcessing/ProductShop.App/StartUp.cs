using AutoMapper;
using ProductShop.App.DTOs;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using DataAnnotations = System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;
using ProductShop.Data;
using System.Linq;
using ProductShop.App.ExportDtos;
using System.Text;
using System.Xml;

namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //Import Data
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            IMapper mapper = configuration.CreateMapper();

            string usersXmlString = File.ReadAllText(@"XMLs\users.xml");
            XmlSerializer usersSerializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            UserDto[] deserializedUsers = (UserDto[])usersSerializer.Deserialize(new StringReader(usersXmlString));
            List<User> users = new List<User>();
            foreach (UserDto userDto in deserializedUsers)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                User user = mapper.Map<User>(userDto);
                users.Add(user);
            }

            ProductShopContext context = new ProductShopContext();
            context.Users.AddRange(users);
            context.SaveChanges();

            string productsXmlString = File.ReadAllText("XMLs/products.xml");
            XmlSerializer productsSerializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            ProductDto[] deserializedProducts = (ProductDto[])productsSerializer.Deserialize(new StringReader(productsXmlString));
            List<Product> products = new List<Product>();
            int counter = 1;
            foreach (ProductDto productDto in deserializedProducts)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                Product product = mapper.Map<Product>(productDto);

                int buyerId = new Random().Next(1, 30);
                int sellerId = new Random().Next(31, 56);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;
                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                counter++;
                products.Add(product);
            }

            //ProductShopContext context = new ProductShopContext();
            context.Products.AddRange(products);
            context.SaveChanges();

            string categoryXmlString = File.ReadAllText("XMLs/categories.xml");
            XmlSerializer categoriesSerializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            CategoryDto[] deserializedCategories = (CategoryDto[])categoriesSerializer.Deserialize(new StringReader(categoryXmlString));
            List<Category> categories = new List<Category>();
            foreach (CategoryDto categoryDto in deserializedCategories)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                Category category = mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }

            //ProductShopContext context = new ProductShopContext();
            context.Categories.AddRange(categories);
            context.SaveChanges();

            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            //for (int productId = 201; productId <= 400; productId++)
            for (int productId = products.Min(p => p.Id); productId <= products.Max(p => p.Id); productId++)
            {
                //int categoryId = new Random().Next(1, 12);
                int categoryId = new Random().Next(categories.Min(c => c.Id), categories.Max(p => p.Id) + 1);
                CategoryProduct categoryProduct = new CategoryProduct()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };

                categoryProducts.Add(categoryProduct);
            }

            //ProductShopContext context = new ProductShopContext();
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            //Export data
            //Query 1. Products in Range
            //ProductShopContext context = new ProductShopContext();
            ExportProductDto[] productsInRangeProducts = context.Products
                .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                .OrderByDescending(p => p.Price)
                .Select(p => new ExportProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName ?? p.Buyer.LastName
                })
                .ToArray();

            StringBuilder productsInRangeBuilder = new StringBuilder();
            XmlSerializerNamespaces productsInRangeXmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer productsInRangeProductsSerializer = new XmlSerializer(typeof(ExportProductDto[]), new XmlRootAttribute("products"));
            productsInRangeProductsSerializer.Serialize(new StringWriter(productsInRangeBuilder), productsInRangeProducts, productsInRangeXmlNamespaces);
            File.WriteAllText("XMLs/products-in-range.xml", productsInRangeBuilder.ToString());

            //Query 2. Sold Products
            //ProductShopContext context = new ProductShopContext();
            ExportUserDto[] soldProductsUsers = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold.Select(ps => new SoldProductDto
                    {
                        Name = ps.Name,
                        Price = ps.Price
                    })
                    .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToArray();

            StringBuilder soldProductsBuilder = new StringBuilder();
            XmlSerializerNamespaces soldProductsXmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer soldProductsProductsSerializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("users"));
            soldProductsProductsSerializer.Serialize(new StringWriter(soldProductsBuilder), soldProductsUsers, soldProductsXmlNamespaces);
            File.WriteAllText("XMLs/users-sold-products.xml", soldProductsBuilder.ToString());

            //Query 3. Categories By Products Count
            //ProductShopContext context = new ProductShopContext();
            ExportCategoryDto[] expotrCategoryDtos = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new ExportCategoryDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price),
                    //AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price)
                    AveragePrice = c.CategoryProducts.Select(cp => cp.Product.Price).DefaultIfEmpty(0).Average()
                })
                .ToArray();

            StringBuilder categoriesByProductsCountBuilder = new StringBuilder();
            XmlSerializerNamespaces categoriesByProductsCountXmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer categoriesByProductsCountProductsSerializer = new XmlSerializer(typeof(ExportCategoryDto[]), new XmlRootAttribute("categories"));
            categoriesByProductsCountProductsSerializer.Serialize(new StringWriter(categoriesByProductsCountBuilder), expotrCategoryDtos, categoriesByProductsCountXmlNamespaces);
            File.WriteAllText("XMLs/categories-by-products.xml", categoriesByProductsCountBuilder.ToString());

            //Query 4. Users and Products
            //ProductShopContext context = new ProductShopContext();
            UsersAndProductsUsersDto usersAndProductsUsersDto = new UsersAndProductsUsersDto
            {
                Count = context.Users.Count(),
                UsersAndProductsUserDtos = context.Users
                    .Where(u => u.ProductsSold.Count >= 1)
                    .Select(u => new UsersAndProductsUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age.ToString(),
                    UsersAndProductsSoldProduct = new UsersAndProductsSoldProduct
                    {
                        Count = u.ProductsSold.Count(),
                        UsersAndProductsProductDtos = u.ProductsSold.Select(p => new UsersAndProductsProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        }).ToArray()
                    }
                }).ToArray()
            };

            StringBuilder usersAndProductsBuilder = new StringBuilder();
            XmlSerializerNamespaces usersAndProductsXmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer usersAndProductsSerializer = new XmlSerializer(typeof(UsersAndProductsUsersDto), new XmlRootAttribute("users"));
            usersAndProductsSerializer.Serialize(new StringWriter(usersAndProductsBuilder), usersAndProductsUsersDto, usersAndProductsXmlNamespaces);
            File.WriteAllText("XMLs/users-and-products.xml", usersAndProductsBuilder.ToString());
        }

        public static bool IsValid(object obj)
        {
            DataAnnotations.ValidationContext validationContext = new DataAnnotations.ValidationContext(obj);
            List <DataAnnotations.ValidationResult> validationResults = new List<DataAnnotations.ValidationResult>();
            bool isValid = DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
