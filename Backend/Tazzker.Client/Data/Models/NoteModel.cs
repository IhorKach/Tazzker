using System.ComponentModel.DataAnnotations;

namespace Tazzker.Client.Data.Models
{
    public class NoteModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
        public bool IsSynced { get; set; } = false;
    }
}
