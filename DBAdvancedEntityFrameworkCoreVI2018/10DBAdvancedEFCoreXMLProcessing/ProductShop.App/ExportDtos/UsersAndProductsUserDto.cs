﻿using System.Xml.Serialization;

namespace ProductShop.App.ExportDtos
{
    [XmlType("user")]
    public class UsersAndProductsUserDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public UsersAndProductsSoldProduct UsersAndProductsSoldProduct { get; set; }
    }
}