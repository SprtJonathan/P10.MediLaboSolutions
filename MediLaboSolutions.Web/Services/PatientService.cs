using MediLaboSolutions.Common.Interfaces;
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

        public async Task<List<IPatient>> GetAllPatientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<IPatient>>("api/patients")
                ?? new List<IPatient>();
        }

        public async Task<IPatient?> GetPatientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<IPatient>($"api/patients/{id}");
        }

        public async Task AddPatientAsync(IPatient patient)
        {
            await _httpClient.PostAsJsonAsync("api/patients", patient);
        }

        public async Task UpdatePatientAsync(int id, IPatient patient)
        {
            await _httpClient.PutAsJsonAsync($"api/patients/{id}", patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/patients/{id}");
        }
    }
}