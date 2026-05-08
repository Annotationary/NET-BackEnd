using Jso.Annotationary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Domain.Entities
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; private set; }

        public string? ProjectName { get; private set; }

        public string? Description { get; private set; }

        public ProjectStatus ProjectStatus { get; private set; } = ProjectStatus.NOT_STARTED;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime LastUpdatedAt { get; private set; }

        // Foreign key to User
        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public ICollection<ProjectMember> ProjectMembers { get; private set; } = new List<ProjectMember>();
    }
}
