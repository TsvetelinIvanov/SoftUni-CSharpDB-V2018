using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFood.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(15, 80)]
        public int Age { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        [Required]
        public Position Position { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
