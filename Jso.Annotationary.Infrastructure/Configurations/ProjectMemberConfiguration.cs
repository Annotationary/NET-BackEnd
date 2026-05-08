using Jso.Annotationary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jso.Annotationary.Infrastructure.Configurations
{
    public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> builder)
        {
            builder.HasKey(pm => pm.ProjectMemberId);

            // Not allow duplicate entries for the same user in the same project
            builder.HasIndex(pm => new { pm.UserId, pm.ProjectId })
                .IsUnique();

            // ProjectMember has one User, with many ProjectMembers
            builder.HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectMember has one Project, with many ProjectMembers
            builder.HasOne(pm => pm.User)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
