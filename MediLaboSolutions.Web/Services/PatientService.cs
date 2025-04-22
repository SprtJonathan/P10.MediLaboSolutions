using MediLaboSolutions.Common.Interfaces;
using MediLaboSolutions.Web.Models.Patients;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MediLaboSolutions.Web.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PatientService> _logger;

        public PatientService(HttpClient httpClient, ILogger<PatientService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7157/");
            _logger = logger;
        }

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            try
            {
                _logger.LogInformation("Récupération de la liste de tous les patients");
                var patients = await _httpClient.GetFromJsonAsync<List<PatientDto>>("api/patients");
                _logger.LogInformation("Liste des patients récupérée avec succès");
                return patients ?? new List<PatientDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de la liste des patients");
                return new List<PatientDto>();
            }
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Récupération du patient avec l'ID {id}");
                var patient = await _httpClient.GetFromJsonAsync<PatientDto>($"api/patients/{id}");
                _logger.LogInformation($"Patient avec l'ID {id} récupéré avec succès");
                return patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération du patient avec l'ID {id}");
                return null;
            }
        }

        public async Task AddPatientAsync(PatientDto patient)
        {
            try
            {
                _logger.LogInformation("Ajout d'un nouveau patient");
                var json = System.Text.Json.JsonSerializer.Serialize(patient);
                _logger.LogInformation("JSON envoyé : {Json}", json);
                var response = await _httpClient.PostAsJsonAsync("api/patients", patient);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Patient ajouté avec succès");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'ajout du patient");
                throw;
            }
        }

        public async Task UpdatePatientAsync(int id, PatientDto patient)
        {
            try
            {
                _logger.LogInformation($"Mise à jour du patient avec l'ID {id}");
                var response = await _httpClient.PutAsJsonAsync($"api/patients/{id}", patient);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Patient avec l'ID {id} mis à jour avec succès");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la mise à jour du patient avec l'ID {id}");
                throw;
            }
        }

        public async Task DeletePatientAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Suppression du patient avec l'ID {id}");
                var response = await _httpClient.DeleteAsync($"api/patients/{id}");
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Patient avec l'ID {id} supprimé avec succès");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression du patient avec l'ID {id}");
                throw;
            }
        }
    }
}