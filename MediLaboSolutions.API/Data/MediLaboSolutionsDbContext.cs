using MediLaboSolutions.API.Models.Patient;
using MediLaboSolutions.Common.Enumerables;
using Microsoft.EntityFrameworkCore;

namespace MediLaboSolutions.API.Data
{
    public class MediLaboSolutionsDbContext : DbContext
    {
        public MediLaboSolutionsDbContext(DbContextOptions<MediLaboSolutionsDbContext> options) : base(options) { }

        public DbSet<PatientEF> Patients { get; set; }
        public DbSet<AdresseEF> Adresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientEF>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateNaissance).IsRequired();
                entity.Property(e => e.Genre);
                entity.Property(e => e.Telephone);

                entity.HasOne(p => p.Adresse)
                      .WithOne()
                      .HasForeignKey<PatientEF>(p => p.AdresseId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<AdresseEF>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Numero);
                entity.Property(e => e.Voie).HasMaxLength(200);
                entity.Property(e => e.Ville).HasMaxLength(100);
                entity.Property(e => e.CodePostal).HasMaxLength(20);
                entity.Property(e => e.Pays).HasMaxLength(100);
            });
        }
    }
}