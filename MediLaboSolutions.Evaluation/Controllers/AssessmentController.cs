using MediLaboSolutions.Evaluation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.Evaluation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssessmentController : ControllerBase
{
    private readonly AssessmentService _assessmentService;

    public AssessmentController(AssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetAssessment(int patientId)
    {
        var result = await _assessmentService.EvaluerRisqueAsync(patientId);
        return Ok(result);
    }
}
