using System.ComponentModel.DataAnnotations;

namespace Tazzker.Client.Data.Models
{
    public class ListModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AssignedDay { get; set; } = null;

        public bool IsDeleted { get; set; } = false;
        public bool IsSynced { get; set; } = false;
    }
}
