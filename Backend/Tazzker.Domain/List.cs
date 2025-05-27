using System.ComponentModel.DataAnnotations;

namespace Tazzker.Domain
{
    public class List
    {
        [Key]
        public Guid ListId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedDay { get; set; }
    }
}
