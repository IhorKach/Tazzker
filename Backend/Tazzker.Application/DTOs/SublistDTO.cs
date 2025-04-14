namespace Tazzker.Application.DTOs
{
    public class SublistDTO
    {
        public Guid SublistId { get; set; }
        public Guid ListId { get; set; }
        public string Title { get; set; } = null!;
        public float Order { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
