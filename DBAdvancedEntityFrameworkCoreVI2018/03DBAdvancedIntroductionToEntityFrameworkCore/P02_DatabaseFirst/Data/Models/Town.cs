﻿using P02_DatabaseFirst.Data.Models;
using System.Collections.Generic;

namespace P02_DatabaseFirst.Data
{
    public class Town
    {
        public Town()
        {
            Addresses = new HashSet<Address>();
        }

        public int TownId { get; set; }

        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}