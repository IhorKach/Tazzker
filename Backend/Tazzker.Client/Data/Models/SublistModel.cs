using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tazzker.Client.Interfaces;

namespace Tazzker.Client.Data.Models
{
	public class SublistModel : ISyncable
    {
		[Key]
        [JsonPropertyName("id")]
        public Guid SublistId { get; set; } = Guid.NewGuid();

		public Guid ListId { get; set; }
		public string Title { get; set; } = "";

		public float Order { get; set; } = 0f;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; } = false;
		public bool IsSynced { get; set; } = false;
	}
}
