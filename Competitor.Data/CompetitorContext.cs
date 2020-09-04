using Competitor.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Competitor.Data
{
    public class CompetitorContext: DbContext, IUnitOfWork
    {
        public CompetitorContext(DbContextOptions<CompetitorContext> option) : base(option)
        {
        }

        // Identities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Custom application mappings
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.SerialNumber).HasMaxLength(450);
                entity.Property(e => e.Username).HasMaxLength(450);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.UserTokens)
                      .HasForeignKey(ut => ut.UserId);

                entity.Property(ut => ut.RefreshTokenIdHash).HasMaxLength(450).IsRequired();
                entity.Property(ut => ut.RefreshTokenIdHashSource).HasMaxLength(450);
            });
        }
    }
}
