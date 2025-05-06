using MediLaboSolutions.Notes.Models.Note;

namespace MediLaboSolutions.Notes.Repositories;

public interface INoteRepository
{
    Task<List<NoteEF>> GetAllAsync();
    Task<NoteEF?> GetByIdAsync(string id);
    Task AddAsync(NoteEF note);
    Task UpdateAsync(NoteEF note);
    Task DeleteAsync(string id);
}
