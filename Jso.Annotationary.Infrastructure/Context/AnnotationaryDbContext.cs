using Jso.Annotationary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Infrastructure.Context
{
    public class AnnotationaryDbContext : DbContext
    {
        public AnnotationaryDbContext(DbContextOptions<AnnotationaryDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
