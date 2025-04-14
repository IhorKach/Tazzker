namespace Tazzker.Application.DTOs
{
    public class ListDTO
    {
        public Guid ListId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedDay { get; set; }
        public bool IsDeleted { get; set; }
    }
}
