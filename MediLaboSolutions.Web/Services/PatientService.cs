using MediLaboSolutions.Data.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MediLaboSolutions.Web.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Patient>>("api/patients")
                ?? new List<Patient>();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Patient>($"api/patients/{id}");
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _httpClient.PostAsJsonAsync("api/patients", patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _httpClient.PutAsJsonAsync($"api/patients/{patient.Id}", patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/patients/{id}");
        }
    }
}