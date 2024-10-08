using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class ProcedureAnimalAid
    {
        [ForeignKey("Procedure")]
        public int ProcedureId { get; set; }
        [Required]
        public Procedure Procedure { get; set; }

        [ForeignKey("AnimalAid")]
        public int AnimalAidId { get; set; }
        [Required]
        public AnimalAid AnimalAid { get; set; }
    }
}
