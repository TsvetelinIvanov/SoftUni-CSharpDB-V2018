using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ProductShop.App.DTOs
{
    [XmlType("product")]
    public class ProductDto
    {
        [MinLength(3)]
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}