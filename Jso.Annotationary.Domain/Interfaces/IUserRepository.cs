using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByIdWithProjectsAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckIsUserActiveAsync(Guid userId);
    }
}
