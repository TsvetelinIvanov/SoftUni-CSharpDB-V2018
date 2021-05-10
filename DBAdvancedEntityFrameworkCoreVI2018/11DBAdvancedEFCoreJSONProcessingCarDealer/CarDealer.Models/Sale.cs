using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public decimal Discount { get; set; }

        [Column("Car_Id")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Column("Customer_Id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}