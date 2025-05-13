using MediLaboSolutions.Notes.Dto;
using MediLaboSolutions.Notes.Models.Note;
using MediLaboSolutions.Notes.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.Notes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _repository;

    public NotesController(INoteRepository repository)
    {
        _repository = repository;
    }

    // GET: api/notes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll()
    {
        var notes = await _repository.GetAllAsync();
        var noteDtos = notes.Select(n => new NoteDto
        {
            Id = n.Id,
            PatientId = n.PatientId,
            PraticienUsername = n.PraticienUsername,
            Texte = n.Texte,
            DateCreation = n.DateCreation
        });
        return Ok(noteDtos);
    }

    // GET: api/notes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetById(string id)
    {
        var note = await _repository.GetByIdAsync(id);
        if (note == null) return NotFound();

        var noteDto = new NoteDto
        {
            Id = note.Id,
            PatientId = note.PatientId,
            PraticienUsername = note.PraticienUsername,
            Texte = note.Texte,
            DateCreation = note.DateCreation
        };
        return Ok(noteDto);
    }

    // POST: api/notes
    [HttpPost]
    public async Task<ActionResult<NoteDto>> Create([FromBody] NoteDto noteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var note = new NoteEF
        {
            PatientId = noteDto.PatientId,
            PraticienUsername = noteDto.PraticienUsername,
            Texte = noteDto.Texte,
            DateCreation = noteDto.DateCreation
        };

        await _repository.AddAsync(note);
        noteDto.Id = note.Id;
        return CreatedAtAction(nameof(GetById), new { id = note.Id }, noteDto);
    }

    // PUT: api/notes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] NoteDto noteDto)
    {
        if (id != noteDto.Id) return BadRequest();
        var note = await _repository.GetByIdAsync(id);
        if (note == null) return NotFound();


        note.PatientId = noteDto.PatientId;
        note.PraticienUsername = noteDto.PraticienUsername;
        note.Texte = noteDto.Texte;
        note.DateCreation = noteDto.DateCreation;

        await _repository.UpdateAsync(note);
        return NoContent();
    }

    // DELETE: api/notes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var note = await _repository.GetByIdAsync(id);
        if (note == null) return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
