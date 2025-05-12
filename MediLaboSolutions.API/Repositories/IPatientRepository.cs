using MediLaboSolutions.API.Models.Patient;

namespace MediLaboSolutions.API.Repositories
{
    public interface IPatientRepository
    {
        Task<List<PatientEF>> GetAllAsync();
        Task<PatientEF?> GetByIdAsync(int id);
        Task AddAsync(PatientEF patient);
        Task UpdateAsync(PatientEF patient);
        Task DeleteAsync(int id);
    }
}