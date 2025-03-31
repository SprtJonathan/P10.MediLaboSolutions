using MediLaboSolutions.API.Data;
using MediLaboSolutions.API.Models.Patient;
using Microsoft.EntityFrameworkCore;

namespace MediLaboSolutions.API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MediLaboSolutionsDbContext _context;

        public PatientRepository(MediLaboSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientEF>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<PatientEF> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task AddAsync(PatientEF patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientEF patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}