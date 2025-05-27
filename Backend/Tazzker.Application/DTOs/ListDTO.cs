using System.Text.Json.Serialization;

namespace Tazzker.Application.DTOs
{
    public class ListDTO
    {
        [JsonPropertyName("id")]
        public Guid ListId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedDay { get; set; }
    }
}
