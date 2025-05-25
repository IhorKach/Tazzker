using System.ComponentModel.DataAnnotations;

namespace Tazzker.Domain
{
    public class Sublist
    {
        [Key]
        public Guid SublistId { get; set; }
        public Guid ListId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public float Order { get; set; }
        //public bool IsDeleted { get; set; }

    }
}
