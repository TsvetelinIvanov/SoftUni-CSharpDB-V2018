using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Part
    {
        public Part()
        {
            this.PartCars = new HashSet<PartCar>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Column("Supplier_Id")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<PartCar> PartCars { get; set; }
    }
}