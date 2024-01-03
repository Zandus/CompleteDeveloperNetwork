using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CompleteDeveloperNetwork.Models;

namespace CompleteDeveloperNetwork.Data
{
    public partial class UserDBContext : DbContext
    {
        public UserDBContext()
        {
        }

        public UserDBContext(DbContextOptions<UserDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=UserDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Hobby)
                    .HasMaxLength(45)
                    .HasColumnName("hobby");

                entity.Property(e => e.Mail)
                    .HasMaxLength(45)
                    .HasColumnName("mail");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(45)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Skillsets)
                    .HasMaxLength(45)
                    .HasColumnName("skillsets");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
