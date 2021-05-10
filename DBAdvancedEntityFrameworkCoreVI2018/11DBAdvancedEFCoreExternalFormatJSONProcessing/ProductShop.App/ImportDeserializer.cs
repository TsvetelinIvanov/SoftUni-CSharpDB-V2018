using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ProductShop.App
{
    public class ImportDeserializer
    {
        private readonly ProductShopContext context;
        private readonly IMapper mapper;

        public ImportDeserializer(ProductShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void ImportUsers()
        {
            string jsonString = File.ReadAllText("Json/users.json");
            User[] deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);
            List<User> users = new List<User>();
            foreach (User user in deserializedUsers)
            {
                if (this.IsValid(user))
                {
                    users.Add(user);
                }                
            }

            this.context.Users.AddRange(users);
            this.context.SaveChanges();
        }

        public void ImportProducts()
        {
            string jsonString = File.ReadAllText("Json/products.json");
            Product[] deserializedProducts = JsonConvert.DeserializeObject<Product[]>(jsonString);
            List<Product> products = new List<Product>();
            foreach (Product product in deserializedProducts)
            {
                if (!this.IsValid(product))
                {
                    continue;
                }

                int sellerId = new Random().Next(1, 35);
                int buyerId = new Random().Next(35, 57);
                int randomCounterNullNumber = new Random().Next(1, 5);

                product.SellerId = sellerId;
                product.BuyerId = buyerId;
                if (randomCounterNullNumber == 3)
                {
                    product.BuyerId = null;
                }

                products.Add(product);
            }

            this.context.Products.AddRange(products);
            this.context.SaveChanges();
        }

        public void ImportCategories()
        {
            string jsonString = File.ReadAllText("Json/categories.json");
            Category[] deserializedCategories = JsonConvert.DeserializeObject<Category[]>(jsonString);
            List<Category> categories = new List<Category>();
            foreach (Category category in deserializedCategories)
            {
                if (!this.IsValid(category))
                {
                    continue;
                }

                categories.Add(category);
            }

            this.context.Categories.AddRange(categories);
            this.context.SaveChanges();
        }

        public void SetCategoryProducts()
        {
            Product[] products = this.context.Products.ToArray();
            Category[] categories = this.context.Categories.ToArray();
            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            for (int productId = 1; productId <= products.Length; productId++)
            {
                int categoryId = new Random().Next(1, categories.Length + 1);
                CategoryProduct categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };

                categoryProducts.Add(categoryProduct);
            }

            this.context.CategoryProducts.AddRange(categoryProducts);
            this.context.SaveChanges();
        }

        private bool IsValid(object obj)
        {
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, results, true);

            return isValid;
        }
    }
}