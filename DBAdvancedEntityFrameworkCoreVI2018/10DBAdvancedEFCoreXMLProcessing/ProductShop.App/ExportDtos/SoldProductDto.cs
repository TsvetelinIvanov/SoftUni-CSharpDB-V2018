﻿using System.Xml.Serialization;

namespace ProductShop.App.ExportDtos
{
    [XmlType("product")]
    public class SoldProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}