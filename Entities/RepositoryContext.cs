using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ValueEntity> Values { get; set; }
        public DbSet<WorkEntity> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WorkWorkEntity>()
                .HasKey(u => new { u.NextWorkId, u.PrevWorkId });

            builder.Entity<WorkEntity>()
                .HasMany(u => u.NextWorks)
                .WithOne(f => f.PrevWork)
                .HasForeignKey(fk => fk.PrevWorkId);

            builder.Entity<WorkEntity>()
                .HasMany(u => u.PrevWorks)
                .WithOne(f => f.NextWork)
                .HasForeignKey(fk => fk.NextWorkId);
        }
    }
}
