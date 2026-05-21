using Jso.Annotationary.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Jso.Annotationary.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? AvatarUrl { get; set; }

        public string? CoverImageUrl { get; set; }

        public string? Specialization { get; set; }

        public UserStatus UserStatus { get; set; } = UserStatus.Active;

        public UserRole UserRole { get; set; } = UserRole.ANNOTATOR;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    }
}
