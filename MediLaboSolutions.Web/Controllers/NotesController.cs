using MediLaboSolutions.Web.Models.Notes;
using MediLaboSolutions.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.Web.Controllers;

[Authorize]
public class NotesController : Controller
{
    private readonly NoteService _noteService;
    private readonly ILogger<NotesController> _logger;

    public NotesController(NoteService noteService, ILogger<NotesController> logger)
    {
        _noteService = noteService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(NoteDto note)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            note.DateCreation = DateTime.Now;
            await _noteService.AddNoteAsync(note);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'ajout de la note");
            return StatusCode(500);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Get(string id)
    {
        var notes = await _noteService.GetAllNotesAsync();
        var note = notes.FirstOrDefault(n => n.Id == id);
        return note == null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, string texte)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(texte))
            return BadRequest("ID ou texte invalide.");

        try
        {
            _logger.LogInformation($"Modification de la note {id}");

            var allNotes = await _noteService.GetAllNotesAsync();
            var note = allNotes.FirstOrDefault(n => n.Id == id);
            if (note == null)
                return NotFound("Note non trouvée");

            note.Texte = texte;

            var result = await _noteService.UpdateNoteAsync(note);
            if (!result)
                return StatusCode(500, "Échec de la mise à jour");

            _logger.LogInformation($"Note {id} modifiée avec succès");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la modification de la note {id}");
            return StatusCode(500, "Erreur serveur");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("ID invalide.");

        try
        {
            _logger.LogInformation($"Suppression de la note {id}");

            var result = await _noteService.DeleteNoteAsync(id);
            if (!result)
                return StatusCode(500, "Échec de la suppression");

            _logger.LogInformation($"Note {id} supprimée avec succès");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de la note {id}");
            return StatusCode(500, "Erreur serveur");
        }
    }
}
