using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.Gateway.Controllers;

[ApiController]
[Route("gateway/status")]
public class StatusController : ControllerBase
{
    private readonly HttpClient _httpClient = new();

    private readonly Dictionary<string, string> _routeHealthChecks = new()
    {
        { "Patients API", "http://medilabosolutions.api/health" },
        { "Notes API", "http://medilabosolutions.notes/health" },
        { "Evaluation API", "http://medilabosolutions.evaluation/health" }
    };

    [HttpGet]
    public async Task<IActionResult> GetRouteStatuses()
    {
        var statuses = new Dictionary<string, string>();

        foreach (var kvp in _routeHealthChecks)
        {
            try
            {
                var response = await _httpClient.GetAsync(kvp.Value);
                statuses[kvp.Key] = response.IsSuccessStatusCode ? "🟢 En ligne" : $"🟠 Réponse: {response.StatusCode}";
            }
            catch
            {
                statuses[kvp.Key] = "🔴 Hors ligne";
            }
        }

        return Ok(statuses);
    }
}
