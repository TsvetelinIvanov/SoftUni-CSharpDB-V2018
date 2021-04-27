using System.Collections.Generic;

namespace PhotoShare.Models
{
    public class Tag
    {
        public Tag()
        {
            this.AlbumTags = new HashSet<AlbumTag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AlbumTag> AlbumTags { get; set; }
    }
}