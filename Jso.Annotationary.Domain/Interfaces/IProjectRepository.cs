using Jso.Annotationary.Domain.Entities;

namespace Jso.Annotationary.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetByUserIdAsync(Guid userId);
        Task<Project?> GetByIdAsync(Guid id);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
    }
}
