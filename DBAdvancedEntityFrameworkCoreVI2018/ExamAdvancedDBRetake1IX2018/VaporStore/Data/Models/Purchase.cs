﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        public string ProductKey { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }
        [Required]
        public Card Card { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        [Required]
        public Game Game { get; set; }
    }
}