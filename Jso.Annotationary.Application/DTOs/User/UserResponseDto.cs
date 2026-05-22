using Jso.Annotationary.Domain.Enums;

namespace Jso.Annotationary.Application.DTOs.User
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string? AvatarUrl { get; set; }

        public string? CoverImageUrl { get; set; }

        public string? Specialization { get; set; }

        public UserStatus UserStatus { get; set; }

        public UserRole UserRole { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
