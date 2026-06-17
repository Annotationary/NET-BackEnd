using Jso.Annotationary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jso.Annotationary.Infrastructure.Context
{
    /// <summary>
    ///
    /// An AnnotationaryDbContext inside the Infrastructure project is the concrete class that handles the actual
    /// database connection, configuration, and data persistence for your application using Entity Framework Core (EF Core).
    ///
    /// It implements the data access layer by mapping your domain entities to database tables and executing the SQL queries
    /// needed to save or retrieve data.
    /// 
    /// </summary>
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
