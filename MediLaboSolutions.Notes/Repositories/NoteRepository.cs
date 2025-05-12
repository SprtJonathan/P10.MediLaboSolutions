using MediLaboSolutions.Notes.Data;
using MediLaboSolutions.Notes.Models.Note;
using Microsoft.EntityFrameworkCore;

namespace MediLaboSolutions.Notes.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly MediLaboMongoDbContext _context;

    public NoteRepository(MediLaboMongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<NoteEF>> GetAllAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<NoteEF?> GetByIdAsync(string id)
    {
        return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task AddAsync(NoteEF note)
    {
        if (string.IsNullOrEmpty(note.Id))
        {
            note.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        }
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NoteEF note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}