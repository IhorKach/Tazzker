using System.Text.Json.Serialization;

namespace Tazzker.Application.DTOs
{
    public class TaskDTO
    {
        [JsonPropertyName("id")]
        public Guid TaskId { get; set; }
        public Guid? SublistId { get; set; }
        public float Order { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }
    }
}
