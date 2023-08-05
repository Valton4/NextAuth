using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NextAuth.EPublication
{
    public partial class ProfessorOnlineContext : DbContext
    {
        public ProfessorOnlineContext()
        {
        }

        public ProfessorOnlineContext(DbContextOptions<ProfessorOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProfessorPublication> ProfessorPublications { get; set; } = null!;
        public virtual DbSet<ProfessorPublicationDetail> ProfessorPublicationDetails { get; set; } = null!;
        public virtual DbSet<ProfessorPublicationForm> ProfessorPublicationForms { get; set; } = null!;
        public virtual DbSet<ProfessorPublicationType> ProfessorPublicationTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=10.10.0.62;Database=ProfessorOnline;User ID=General;Password=!General2023!;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessorPublication>(entity =>
            {
                entity.ToTable("ProfessorPublication");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProfessorPublicationDetail>(entity =>
            {
                entity.ToTable("ProfessorPublicationDetail");

                entity.HasOne(d => d.PublicationForm)
                    .WithMany(p => p.ProfessorPublicationDetails)
                    .HasForeignKey(d => d.PublicationFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Professor__Publi__1D114BD1");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.ProfessorPublicationDetails)
                    .HasForeignKey(d => d.PublicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Professor__Publi__25A691D2");
            });

            modelBuilder.Entity<ProfessorPublicationForm>(entity =>
            {
                entity.ToTable("ProfessorPublicationForm");

                entity.Property(e => e.Field).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<ProfessorPublicationType>(entity =>
            {
                entity.ToTable("ProfessorPublicationType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
