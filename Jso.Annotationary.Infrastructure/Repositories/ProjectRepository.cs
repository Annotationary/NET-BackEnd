using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AnnotationaryDbContext _context;

        public ProjectRepository(AnnotationaryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project project)
        {
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Project?> GetByIdAsync(Guid id)
        {
            return _context.Projects.FindAsync(id).AsTask();
        }

        public async Task<List<Project>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Projects
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
