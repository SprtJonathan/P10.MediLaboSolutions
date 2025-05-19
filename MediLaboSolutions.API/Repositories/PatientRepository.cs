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
                // Vérification de la présence d'une adresse identique
                var existingAdresse = await _context.Adresses.FirstOrDefaultAsync(a =>
                    a.Numero == patient.Adresse.Numero &&
                    a.Voie == patient.Adresse.Voie &&
                    a.Ville == patient.Adresse.Ville &&
                    a.CodePostal == patient.Adresse.CodePostal &&
                    a.Pays == patient.Adresse.Pays);

                if (existingAdresse != null)
                {
                    // Si une adresse identique est trouvée, alors on l'associe au patient
                    patient.Adresse = null;
                    patient.AdresseId = existingAdresse.Id;
                }
                else
                {
                    // Sinon on ajoute la nouvelle adresse
                    _context.Adresses.Add(patient.Adresse);
                    await _context.SaveChangesAsync();
                    patient.AdresseId = patient.Adresse.Id;
                }
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientEF patient)
        {
            var existingPatient = await _context.Patients
                .AsNoTracking()
                .Include(p => p.Adresse)
                .FirstOrDefaultAsync(p => p.Id == patient.Id);

            AdresseEF? oldAdresse = existingPatient?.Adresse;

            if (patient.Adresse != null)
            {
                var existingAdresse = await _context.Adresses.FirstOrDefaultAsync(a =>
                a.Numero == patient.Adresse.Numero &&
                a.Voie == patient.Adresse.Voie &&
                a.Ville == patient.Adresse.Ville &&
                a.CodePostal == patient.Adresse.CodePostal &&
                a.Pays == patient.Adresse.Pays);

                if (existingAdresse != null)
                {
                    patient.Adresse = null;
                    patient.AdresseId = existingAdresse.Id;
                }
                else
                {
                    _context.Adresses.Add(patient.Adresse);
                    await _context.SaveChangesAsync();
                    patient.AdresseId = patient.Adresse.Id;
                }
            }

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            if (oldAdresse != null)
            {
                bool stillUsed = await _context.Patients.AnyAsync(p => p.AdresseId == oldAdresse.Id);
                if (!stillUsed)
                {
                    var adresseToDelete = await _context.Adresses.FindAsync(oldAdresse.Id);
                    if (adresseToDelete != null)
                    {
                        _context.Adresses.Remove(adresseToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.Include(p => p.Adresse).FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                var adresse = patient.Adresse;

                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();

                // Si l'adresse existe et qu'elle n'est plus utilisée par d'autres patients, on peut la supprimer
                if (adresse != null)
                {
                    bool isAdresseUsed = await _context.Patients.AnyAsync(p => p.AdresseId == adresse.Id);
                    if (!isAdresseUsed)
                    {
                        _context.Adresses.Remove(adresse);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

    }
}
