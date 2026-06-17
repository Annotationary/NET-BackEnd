using Jso.Annotationary.Domain.Enums;

namespace Jso.Annotationary.Application.DTOs.User
{
    /// <summary>
    ///
    /// A simple, lightweight object used to format and return user data back to the client application or API endpoint.
    ///
    /// Acts as a secure, optimized data skin that strips away sensitive information and internal database details before
    /// the data leaves your system.
    /// 
    /// </summary>
    
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
