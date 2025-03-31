using MongoDB.Driver;
using MediLaboSolutions.Data.Models;

namespace MediLaboSolutions.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IMongoCollection<Patient> _patients;

        public PatientRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MediLaboDb");
            _patients = database.GetCollection<Patient>("Patients");
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _patients.Find(_ => true).ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _patients.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Patient patient)
        {
            await _patients.InsertOneAsync(patient);
        }

        public async Task UpdateAsync(Patient patient)
        {
            await _patients.ReplaceOneAsync(p => p.Id == patient.Id, patient);
        }

        public async Task DeleteAsync(int id)
        {
            await _patients.DeleteOneAsync(p => p.Id == id);
        }
    }
}