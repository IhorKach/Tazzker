using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tazzker.Client.Interfaces;

namespace Tazzker.Client.Data.Models
{
    public class TaskModel : ISyncable
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? SublistId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public bool IsCompleted { get; set; } = false;
        public float Order { get; set; } = 0f;

        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsSynced { get; set; } = false;
        public bool PermDeleted { get; set; } = false;
    }

}
