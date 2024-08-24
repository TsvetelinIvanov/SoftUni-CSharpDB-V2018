using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class FastFoodDbContext : DbContext
    {
	public FastFoodDbContext()
	{

	}

	public FastFoodDbContext(DbContextOptions options) : base(options)
	{

	}
        
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categories { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder builder)
	{
	    if (!builder.IsConfigured)
	    {
		builder.UseSqlServer(Configuration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
            builder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ItemId });

            builder.Entity<OrderItem>().HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
              //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderItem>().HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId);
              //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Item>().HasIndex(i => i.Name).IsUnique();

            builder.Entity<Position>().HasIndex(p => p.Name).IsUnique();
	}
    }
}
