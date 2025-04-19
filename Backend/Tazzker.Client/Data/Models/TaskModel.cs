	using System.ComponentModel.DataAnnotations;

namespace Tazzker.Client.Data.Models
{
	public class TaskModel
	{
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string SublistId { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public bool IsCompleted { get; set; } = false;
        public float Order { get; set; } = 0f;

        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
        public bool IsSynced { get; set; } = false;
    }

}
