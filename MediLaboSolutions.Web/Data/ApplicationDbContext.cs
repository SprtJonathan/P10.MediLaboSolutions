using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediLaboSolutions.Web.Models.Patients;

namespace MediLaboSolutions.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MediLaboSolutions.Web.Models.Patients.PatientDto> PatientDto { get; set; } = default!;
    }
}
