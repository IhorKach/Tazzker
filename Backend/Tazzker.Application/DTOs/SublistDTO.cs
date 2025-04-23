using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tazzker.Application.DTOs
{
    public class SublistDTO
    {
        [JsonPropertyName("id")]
        public Guid SublistId { get; set; }
        public Guid ListId { get; set; }
        public string Title { get; set; } = null!;
        public float Order { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}