using System.Net.Http.Json;
using MediLaboSolutions.Common.Enumerables;

namespace MediLaboSolutions.Web.Services;

public class AssessmentService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AssessmentService> _logger;

    public AssessmentService(HttpClient httpClient, ILogger<AssessmentService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ENiveauRisque> GetRiskLevelAsync(int patientId)
    {
        try
        {
            _logger.LogInformation("Appel du microservice d'évaluation pour patient ID {id}", patientId);
            var risk = await _httpClient.GetFromJsonAsync<ENiveauRisque?>($"api/assessment/{patientId}");
            return risk ?? ENiveauRisque.None;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du niveau de risque pour patient ID {id}", patientId);
            return ENiveauRisque.None;
        }
    }
}
