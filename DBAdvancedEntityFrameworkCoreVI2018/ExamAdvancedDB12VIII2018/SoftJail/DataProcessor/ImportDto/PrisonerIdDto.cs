using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Prisoner")]
    public class PrisonerIdDto
    {
        [XmlAttribute]
        public int Id { get; set; }
    }
}