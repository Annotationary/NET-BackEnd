using Jso.Annotationary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
