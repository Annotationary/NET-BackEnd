using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Application.Users.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Specialization { get; set; }
        public string? UserStatus { get; set; }
        public string? UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
