﻿using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class DepartmentDto
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        public CellDto[] Cells { get; set; }
    }
}