using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Domain.Interfaces
{
    /// <summary>
    ///
    /// An IUserRepository in a Domain Project is an interface that defines the data operations allowed for a User entity
    /// without specifying the underlying database. It acts as a contract that the domain layer uses to load,
    /// save, or delete user data, keeping the core business logic independent of external databases.
    /// 
    /// </summary>
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        
        Task<User?> GetByIdAsync(Guid id);
        
        Task<User?> GetByIdWithProjectsAsync(Guid id);
        
        Task AddAsync(User user);
        
        Task UpdateAsync(User user);
        
        Task DeleteAsync(Guid id);
        
        Task SaveChangeAsync();
        
        Task<User> GetByEmailAsync(string email);
        
        Task<bool> CheckIsUserActiveAsync(Guid userId);
    }
}
