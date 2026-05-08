using Jso.Annotationary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
