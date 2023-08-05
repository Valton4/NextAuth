using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextAuth.EPublication;
using NextAuth.Models;
using System.Reflection.Emit;

namespace NextAuth.Data
{
    public class Context : IdentityDbContext<ProfessorUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Publications> Publications { get; set; }
        public virtual DbSet<ProfessorPublication> ProfessorPublications { get; set; } = null!;
        public virtual DbSet<ProfessorPublicationDetail> ProfessorPublicationDetails { get; set; } = null!;
        public virtual DbSet<ProfessorPublicationForm> ProfessorPublicationForms { get; set; } = null!;
        public virtual DbSet<ProfessorPublicationType> ProfessorPublicationTypes { get; set; } = null!;
        public DbSet<ProfessorUser> professorUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProfessorPublication>(entity =>
            {
                entity.ToTable("ProfessorPublication");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            builder.Entity<ProfessorPublicationDetail>(entity =>
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

            builder.Entity<ProfessorPublicationForm>(entity =>
            {
                entity.ToTable("ProfessorPublicationForm");

                entity.Property(e => e.Field).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            builder.Entity<ProfessorPublicationType>(entity =>
            {
                entity.ToTable("ProfessorPublicationType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        public void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
                new IdentityRole() { Name = "HR", ConcurrencyStamp = "3", NormalizedName = "HR" }
                );
        }
    }
}
