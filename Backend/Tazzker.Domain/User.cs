using System.ComponentModel.DataAnnotations;

namespace Tazzker.Domain
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = null!;
        //public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

//refresh token field
        public string? RefreshToken { get; set; }
    }
}
