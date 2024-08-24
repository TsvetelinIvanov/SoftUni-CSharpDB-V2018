using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
	private const string FailureMessage = "Invalid data format.";
	private const string SuccessMessage = "Record {0} successfully imported.";

	public static string ImportEmployees(FastFoodDbContext context, string jsonString)
	{            
            EmployeeDto[] deserializedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            StringBuilder importEmployeesMessageBuilder = new StringBuilder();
            List<Employee> employees = new List<Employee>();
            foreach (EmployeeDto employeeDto in deserializedEmployees)
            {
                if (!IsValid(employeeDto))
                {
                    importEmployeesMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }                

                Position position = GetPosition(context, employeeDto.Position);
                Employee employee = new Employee
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age, 
                    Position = position
                };

                employees.Add(employee);
                importEmployeesMessageBuilder.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return importEmployeesMessageBuilder.ToString().TrimEnd();
	}

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            ItemDto[] deserializedItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            StringBuilder importItemsMessageBuilder = new StringBuilder();
            List<Item> items = new List<Item>();
            List<string> itemNames = new List<string>();
            foreach (ItemDto itemDto in deserializedItems)
            {
                if (!IsValid(itemDto))
                {
                    importItemsMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }

                bool itemExists = context.Items.Any(i => i.Name == itemDto.Name);
                bool itemNameExists = itemNames.Contains(itemDto.Name);
                if (itemExists || itemNameExists)
                {
                    importItemsMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }

                itemNames.Add(itemDto.Name);

                Category category = GetCategory(context, itemDto.Category);
                Item item = new Item
                {
                    Name = itemDto.Name,
                    Category = category,
                    Price = itemDto.Price
                };

                items.Add(item);
                importItemsMessageBuilder.AppendLine(string.Format(SuccessMessage, item.Name));
            }

            context.Items.AddRange(items);
            context.SaveChanges();

            return importItemsMessageBuilder.ToString().TrimEnd();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
	{
            XmlSerializer serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            OrderDto[] deserializedOrders = (OrderDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder importOrdersMessageBuilder = new StringBuilder();
            List<OrderItem> orderItems = new List<OrderItem>();
            List<Order> orders = new List<Order>();
            foreach (OrderDto orderDto in deserializedOrders)
            {
                if (!IsValid(orderDto))
                {
                    importOrdersMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }

                bool isValidItem = true;
                foreach (OrderItemDto itemDto in orderDto.OrderItems)
                {
                    if (!IsValid(itemDto))
                    {
                        importOrdersMessageBuilder.AppendLine(FailureMessage);
                        isValidItem = false;
                        break;
                    }
                }

                if (!isValidItem)
                {
                    //importOrdersMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }

                Employee employee = context.Employees.FirstOrDefault(e => e.Name == orderDto.Employee);
                if (employee == null)
                {
                    importOrdersMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }

                bool itemsExist = ItemsExist(context, orderDto.OrderItems);
                if (!itemsExist)
                {
                    importOrdersMessageBuilder.AppendLine(FailureMessage);
                    continue;
                }

                DateTime date = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                //bool isDateValid = DateTime.TryParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                //if (!isDateValid)
                //{
                //    importOrdersMessageBuilder.AppendLine(FailureMessage);
                //    continue;
                //}

                OrderType orderType = Enum.Parse<OrderType>(orderDto.Type);
                //bool isOrderTypeValid = Enum.TryParse(orderDto.Type, out OrderType orderType);
                //if (!isOrderTypeValid)
                //{
                //    importOrdersMessageBuilder.AppendLine(FailureMessage);
                //    continue;
                //}

                Order order = new Order
                {
                    Customer = orderDto.Customer,
                    DateTime = date,
                    Type = orderType,
                    Employee = employee
                };

                orders.Add(order);

                foreach (OrderItemDto itemDto in orderDto.OrderItems)
                {
                    Item item = context.Items.FirstOrDefault(i => i.Name == itemDto.Name);
                    OrderItem orderItem = new OrderItem
                    {
                        Order = order,
                        Item = item,
                        Quantity = itemDto.Quantity
                    };

                    orderItems.Add(orderItem);
                }

                importOrdersMessageBuilder.AppendLine($"Order for {orderDto.Customer} on {date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)} added");
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();

            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();

            return importOrdersMessageBuilder.ToString().TrimEnd();
	}        

        private static Position GetPosition(FastFoodDbContext context, string positionName)
        {
            Position position = context.Positions.FirstOrDefault(p => p.Name == positionName);
            if (position == null)
            {
                position = new Position
                {
                    Name = positionName
                };

                context.Positions.Add(position);
                context.SaveChanges();
            }

            return position;
        }

        private static Category GetCategory(FastFoodDbContext context, string categoryName)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                category = new Category
                {
                    Name = categoryName
                };

                context.Categories.Add(category);
                context.SaveChanges();
            }

            return category;
        }

        private static bool ItemsExist(FastFoodDbContext context, OrderItemDto[] orderItems)
        {
            foreach (OrderItemDto item in orderItems)
            {
                bool itemExists = context.Items.Any(i => i.Name == item.Name);
                if(!itemExists)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
