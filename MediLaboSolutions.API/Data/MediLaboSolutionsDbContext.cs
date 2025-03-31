using MediLaboSolutions.API.Models.Patient;
using Microsoft.EntityFrameworkCore;

namespace MediLaboSolutions.API.Data
{
    public class MediLaboSolutionsDbContext : DbContext
    {
        public MediLaboSolutionsDbContext(DbContextOptions<MediLaboSolutionsDbContext> options) : base(options) { }

        public DbSet<PatientEF> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration optionnelle pour PatientEF
            modelBuilder.Entity<PatientEF>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateNaissance).IsRequired();
                entity.Property(e => e.Genre).IsRequired().HasMaxLength(10);
                entity.Property(e => e.AdressePostale).HasMaxLength(200);
                entity.Property(e => e.Telephone);
            });
        }
    }
}