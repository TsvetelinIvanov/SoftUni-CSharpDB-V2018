using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class OfficerPrisoner
    {
        [ForeignKey("Prisoner")]
        public int PrisonerId { get; set; }
        [Required]
        public Prisoner Prisoner { get; set; }

        [ForeignKey("Officer")]
        public int OfficerId { get; set; }
        [Required]
        public Officer Officer { get; set; }
    }
}