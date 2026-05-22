using Jso.Annotationary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jso.Annotationary.Infrastructure.Context
{
    public class AnnotationaryDbContext : DbContext
    {
        public AnnotationaryDbContext(DbContextOptions<AnnotationaryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // User -> ProjectMembers
            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Project -> ProjectMembers
            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
