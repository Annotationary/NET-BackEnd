using Jso.Annotationary.Application.DTOs.User;

namespace Jso.Annotationary.Application.Interfaces
{
    /// <summary>
    ///
    /// An interface that defines the core use cases and business operations available for managing users.
    /// 
    /// </summary>
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> GetByIdAsync(Guid id);
        Task AddAsync(CreateUserDto createUserDto);
        Task UpdateAsync(Guid id, UpdateUserDto updateUserDto);
        Task DeleteAsync(Guid id);
    }
}
