using System.Xml.Serialization;

namespace ProductShop.App.ExportDtos
{
    [XmlType("sold-products")]
    public class UsersAndProductsSoldProduct
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public UsersAndProductsProductDto[] UsersAndProductsProductDtos { get; set; }
    }
}