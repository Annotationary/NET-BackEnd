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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnnotationaryDbContext).Assembly);
        }
    }
}
