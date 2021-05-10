using System.Xml.Serialization;

namespace ProductShop.App.ExportDtos
{
    [XmlRoot("users")]
    public class UsersAndProductsUsersDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UsersAndProductsUserDto[] UsersAndProductsUserDtos { get; set; }
    }
}