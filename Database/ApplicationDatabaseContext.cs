using Graphite.Source.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Graphite.Database
{
    public class ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Dataframe> Dataframes => Set<Dataframe>();
        public DbSet<DataframeLine> DataframeLines => Set<DataframeLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).HasMaxLength(100);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Organizations)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Dataframe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.File).IsRequired().HasMaxLength(255);
                entity.HasOne(e => e.Organization)
                      .WithMany(o => o.Dataframes)
                      .HasForeignKey(e => e.OrgId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DataframeLine>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Data).IsRequired();
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Dataframe)
                      .WithMany(d => d.DataframeLines)
                      .HasForeignKey(e => e.DataframeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}