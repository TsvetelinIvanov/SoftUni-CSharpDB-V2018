using System.Linq;
using FastFood.Data;
using FastFood.Models;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
	public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	{
            Item item = context.Items.FirstOrDefault(i => i.Name == itemName);
            if (item == null)
            {
                return $"Item {itemName} not found!";
            }
            else
            {
                decimal oldPrice = item.Price;
                item.Price = newPrice;

                context.Items.Update(item);
                context.SaveChanges();

                return $"{item.Name} Price updated from ${oldPrice:f2} to ${item.Price:f2}";
            }
	}
    }
}
