using System.ComponentModel.DataAnnotations;

namespace Tazzker.Domain
{
    public class Note
    {
        [Key]
        public Guid NoteId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
