﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamBuilder.Models
{
    public class Team
    {
        [MinLength(0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        //Unique
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Acronym { get; set; }

        [MinLength(0)]
        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<UserTeam> Members { get; set; } = new List<UserTeam>();

        public virtual ICollection<TeamEvent> Events { get; set; } = new List<TeamEvent>();
    }
}