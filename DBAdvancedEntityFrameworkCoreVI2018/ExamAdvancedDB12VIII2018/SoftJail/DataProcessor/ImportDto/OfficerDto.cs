using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerDto
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [XmlElement("Money")]
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Money { get; set; }

        [XmlElement("Position")]
        [Required]
        [RegularExpression(@"(^Overseer$)|(^Guard$)|(^Watcher$)|(^Labour$)")]
        public string Position { get; set; } 

        [XmlElement("Weapon")]
        [Required]
        [RegularExpression(@"(^Knife$)|(^FlashPulse$)|(^ChainRifle$)|(^Pistol$)|(^Sniper$)")]
        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }
        
        [XmlArray]
        public PrisonerIdDto[] Prisoners { get; set; }
    }
}