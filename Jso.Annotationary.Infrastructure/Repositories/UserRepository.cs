using Jso.Annotationary.Domain.Entities;
using Jso.Annotationary.Domain.Enums;
using Jso.Annotationary.Domain.Interfaces;
using Jso.Annotationary.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Jso.Annotationary.Infrastructure.Repositories
{
    /// <summary>
    ///
    /// The UserRepository inside the Infrastructure project is the concrete class that implements the IUserRepository interface
    /// defined in your Domain project. It acts as the actual worker that uses the AnnotationaryDbContext to query and
    /// save user data to the physical database.
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AnnotationaryDbContext _context;

        public UserRepository(AnnotationaryDbContext context)
        {
            _context = context;
        }

        // Get all user
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Add new user
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Delete user by userId
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Get user detail by userId
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Get current user
        public async Task<User?> GetByIdWithProjectsAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        // Update current user
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        
        // Check if current email exists
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Check if current user is active or not
        public async Task<bool> CheckIsUserActiveAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user != null && user.UserStatus == UserStatus.Active;
        }

        // Save changes
        public Task SaveChangeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
