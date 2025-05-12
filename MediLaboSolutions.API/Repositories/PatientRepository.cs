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
            return await _context.Patients.Include(p => p.Adresse).ToListAsync();
        }

        public async Task<PatientEF?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Adresse)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(PatientEF patient)
        {
            if (patient.Adresse != null)
            {
                _context.Adresses.Add(patient.Adresse);
                await _context.SaveChangesAsync();
                patient.AdresseId = patient.Adresse.Id;
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientEF patient)
        {
            if (patient.Adresse != null)
            {
                if (patient.Adresse.Id == 0)
                {
                    _context.Adresses.Add(patient.Adresse);
                    await _context.SaveChangesAsync();
                    patient.AdresseId = patient.Adresse.Id;
                }
                else
                {
                    _context.Adresses.Update(patient.Adresse);
                }
            }

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.Include(p => p.Adresse).FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                if (patient.Adresse != null)
                {
                    _context.Adresses.Remove(patient.Adresse);
                }

                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
