using Graphite.Source.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Graphite.Database
{
    public class ApplicationDatabaseContext : IdentityDbContext<User>
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Dataframe> Dataframes => Set<Dataframe>();
        public DbSet<DataframeLine> DataframeLines => Set<DataframeLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();

            modelBuilder.Entity<Dataframe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SpreadsheetFilePath).HasMaxLength(255);
                entity.HasOne(e => e.User)
                      .WithMany(o => o.Dataframes)
                      .HasForeignKey(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DataframeLine>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Dataframe)
                      .WithMany(d => d.DataframeLines)
                      .HasForeignKey(e => e.DataframeId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}