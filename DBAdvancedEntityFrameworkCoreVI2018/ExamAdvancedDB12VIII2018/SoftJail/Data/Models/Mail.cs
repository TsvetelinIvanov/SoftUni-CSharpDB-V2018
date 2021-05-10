﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z 0-9]+ str.$")]
        public string Address { get; set; }

        [ForeignKey("Prisoner")]
        public int PrisonerId { get; set; }
        [Required]
        public Prisoner Prisoner { get; set; }
    }
}