﻿using System.Xml.Serialization;

namespace CarDealer.DTOs.ExportDTOs
{
    [XmlType("customer")]
    public class ExportCustomerDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}