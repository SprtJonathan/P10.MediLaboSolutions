using MediLaboSolutions.Web.Models.Notes;
using System.Net.Http.Json;

namespace MediLaboSolutions.Web.Services
{
    public class NoteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NoteService> _logger;

        public NoteService(HttpClient httpClient, ILogger<NoteService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7157/");
            _logger = logger;
        }

        public async Task<List<NoteDto>> GetNotesByPatientIdAsync(int patientId)
        {
            try
            {
                _logger.LogInformation($"Récupération des notes pour le patient ID {patientId}");
                var allNotes = await _httpClient.GetFromJsonAsync<List<NoteDto>>("api/notes");
                var filtered = allNotes?.Where(n => n.PatientId == patientId).ToList() ?? new();
                _logger.LogInformation($"{filtered.Count} notes récupérées pour le patient ID {patientId}");
                return filtered;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération des notes pour le patient ID {patientId}");
                return new List<NoteDto>();
            }
        }

        public async Task AddNoteAsync(NoteDto note)
        {
            try
            {
                _logger.LogInformation($"Ajout d'une nouvelle note pour le patient ID {note.PatientId}");
                var response = await _httpClient.PostAsJsonAsync("api/notes", note);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Échec de la requête POST : {StatusCode} - {Message}", response.StatusCode, content);
                    throw new HttpRequestException($"Erreur API : {response.StatusCode} - {content}");
                }

                _logger.LogInformation("Note ajoutée avec succès");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de l'ajout de la note pour le patient ID {note.PatientId}");
                throw;
            }
        }

        public async Task<List<NoteDto>> GetAllNotesAsync()
        {
            try
            {
                _logger.LogInformation("Récupération de toutes les notes");
                var notes = await _httpClient.GetFromJsonAsync<List<NoteDto>>("api/notes");
                return notes ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des notes");
                return new();
            }
        }

        public async Task<bool> UpdateNoteAsync(NoteDto note)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/notes/{note.Id}", note);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la mise à jour de la note {note.Id}");
                return false;
            }
        }

        public async Task<bool> DeleteNoteAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/notes/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression de la note {id}");
                return false;
            }
        }
    }
}
