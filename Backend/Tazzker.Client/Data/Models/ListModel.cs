using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tazzker.Client.Interfaces;

namespace Tazzker.Client.Data.Models
{
    public class ListModel : ISyncable
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid ListId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
        public bool IsSynced { get; set; } = false;
    }
}
