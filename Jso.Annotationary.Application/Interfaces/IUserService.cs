using Jso.Annotationary.Application.DTOs.User;
using Jso.Annotationary.Domain.Response;

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
        Task<Result<UserResponseDto>> GetByIdAsync(Guid id);
        Task<Result> AddAsync(CreateUserDto createUserDto);
        Task<Result> UpdateAsync(Guid id, UpdateUserDto updateUserDto);
        Task<Result> DeleteAsync(Guid id);
    }
}
