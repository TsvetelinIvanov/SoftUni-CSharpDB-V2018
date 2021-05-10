using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsImporter { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}