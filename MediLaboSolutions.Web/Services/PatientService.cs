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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientService(HttpClient httpClient, ILogger<PatientService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        private void CopyAuthCookieToRequest()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies[".AspNetCore.Identity.Application"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Remove("Cookie");
                _httpClient.DefaultRequestHeaders.Add("Cookie", $".AspNetCore.Identity.Application={accessToken}");
            }
        }

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            CopyAuthCookieToRequest();

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
            CopyAuthCookieToRequest();

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
            CopyAuthCookieToRequest();

            try
            {
                _logger.LogInformation("Ajout d'un nouveau patient");
                var response = await _httpClient.PostAsJsonAsync("api/patients", patient);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Échec de la requête POST : {StatusCode} - {Message}", response.StatusCode, content);
                    throw new HttpRequestException($"Erreur API : {response.StatusCode} - {content}");
                }

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
            CopyAuthCookieToRequest();

            try
            {
                _logger.LogInformation($"Mise à jour du patient avec l'ID {id}");
                var response = await _httpClient.PutAsJsonAsync($"api/patients/{id}", patient);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Échec de la requête POST : {StatusCode} - {Message}", response.StatusCode, content);
                    throw new HttpRequestException($"Erreur API : {response.StatusCode} - {content}");
                }

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
            CopyAuthCookieToRequest();

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