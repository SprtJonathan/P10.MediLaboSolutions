using MediLaboSolutions.Common.Interfaces;

namespace MediLaboSolutions.Web.Models.Notes;

public class NoteDto : INote
{
    public string? Id { get; set; }
    public int PatientId { get; set; }
    public required string PraticienUsername { get; set; }
    public required string Texte { get; set; }
    public DateTime DateCreation { get; set; }
}