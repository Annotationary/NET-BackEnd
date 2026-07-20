using Jso.Annotationary.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Jso.Annotationary.Domain.Entities
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public string? Description { get; set; }

        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.NOT_STARTED;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastUpdatedAt { get; set; }

        // Foreign key to User
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    }
}
