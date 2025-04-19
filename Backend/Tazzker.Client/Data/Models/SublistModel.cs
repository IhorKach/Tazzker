using System.ComponentModel.DataAnnotations;

namespace Tazzker.Client.Data.Models
{
	public class SublistModel
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string ListId { get; set; } = ""; // ссылка на родительский список
		public string Title { get; set; } = "";

		public float Order { get; set; } = 0f;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; } = false;
		public bool IsSynced { get; set; } = false;
	}
}
