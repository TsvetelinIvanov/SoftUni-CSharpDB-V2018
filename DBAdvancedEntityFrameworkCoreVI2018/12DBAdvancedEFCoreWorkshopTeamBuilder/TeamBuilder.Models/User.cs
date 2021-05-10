using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            this.CreatedEvents = new List<Event>();
            this.MembersOf = new List<UserTeam>();
            this.CreatedUserTeams = new List<UserTeam>();
            this.ReceivedInvitations = new List<Invitation>();
        }

        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        //Unique
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }

        public GenderEnum Gender { get; set; }

        [MinLength(0)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; }

        public virtual ICollection<UserTeam> MembersOf { get; set; }

        public virtual ICollection<UserTeam> CreatedUserTeams { get; set; }

        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}