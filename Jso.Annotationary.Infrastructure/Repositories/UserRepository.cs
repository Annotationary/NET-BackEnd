using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Jso.Annotationary.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AnnotationaryDbContext _context;

        public UserRepository(AnnotationaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByIdWithProjectsAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }
    }
}
