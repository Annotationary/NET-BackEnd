using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Domain.Entities
{
    public class ProjectMember
    {
        [Key]
        public Guid ProjectMemberId { get; set; }

        // Foreign key to User
        public Guid UserId { get; set; }
        public User? User { get; set; }

        // Foreign key to Project
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
