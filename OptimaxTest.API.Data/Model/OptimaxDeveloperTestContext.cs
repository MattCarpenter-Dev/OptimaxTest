using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OptimaxTest.API.Data.Model
{
    public partial class OptimaxDeveloperTestContext : DbContext
    {
        public OptimaxDeveloperTestContext()
        {
        }

        public OptimaxDeveloperTestContext(DbContextOptions<OptimaxDeveloperTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserRolePermission> UserRolePermissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeDeactivated).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeDeacticated).HasColumnType("datetime");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeDeacticated).HasColumnType("datetime");

                entity.Property(e => e.UserRoleName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRolePermission>(entity =>
            {
                entity.ToTable("UserRole_Permission");

                entity.Property(e => e.UserRolePermissionId).HasColumnName("UserRole_PermissionID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserRolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK__UserRole___Permi__22AA2996");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserRolePermissions)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK__UserRole___UserR__21B6055D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
