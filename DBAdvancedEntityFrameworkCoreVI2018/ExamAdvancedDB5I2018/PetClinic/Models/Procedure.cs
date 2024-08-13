using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PetClinic.Models
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Animal")]
        public int AnimalId { get; set; }
        [Required]
        public Animal Animal { get; set; }

        [ForeignKey("Vet")]
        public int VetId { get; set; }
        [Required]
        public Vet Vet { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; } = new HashSet<ProcedureAnimalAid>();

        [NotMapped]
        //public decimal Cost { get; set; }
        public decimal Cost => this.ProcedureAnimalAids.Sum(paa => paa.AnimalAid.Price);

        [Required]
        public DateTime DateTime { get; set; }
    }
}
