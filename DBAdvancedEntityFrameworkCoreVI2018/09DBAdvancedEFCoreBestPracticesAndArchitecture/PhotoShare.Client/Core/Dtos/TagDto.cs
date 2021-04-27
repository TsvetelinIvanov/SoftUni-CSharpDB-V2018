using PhotoShare.Client.Core.Validation;

namespace PhotoShare.Client.Core.Dtos
{
    public class TagDto
    {
        public int Id { get; set; }

        [Tag]
        public string Name { get; set; }
    }
}