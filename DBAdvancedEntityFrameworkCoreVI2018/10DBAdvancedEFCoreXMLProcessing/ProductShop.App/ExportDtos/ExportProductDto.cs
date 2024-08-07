﻿using System.Xml.Serialization;

namespace ProductShop.App.ExportDtos
{
    [XmlType("product")]
    public class ExportProductDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("buyer")]
        public string Buyer { get; set; }
    }
}