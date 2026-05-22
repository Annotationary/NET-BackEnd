using Jso.Annotationary.Application.DTOs.User;
using Jso.Annotationary.Application.Interfaces;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Manual mapping method to convert User entity to UserResponseDto
        private UserResponseDto MapToResponse(User user)
        {
            return new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                CoverImageUrl = user.CoverImageUrl,
                Specialization = user.Specialization,
                UserStatus = user.UserStatus,
                UserRole = user.UserRole,
                CreatedAt = user.CreatedAt
            };
        }

        public Task AddAsync(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToResponse).ToList();
        }

        public Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
