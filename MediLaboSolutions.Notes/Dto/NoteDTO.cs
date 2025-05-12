using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Common.Interfaces;

namespace MediLaboSolutions.Notes.Dto;

public class NoteDto : INote
{
    public string? Id { get; set; }
    public int PatientId { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string PraticienUsername { get; set; }
    public required string Texte { get; set; }
    public DateTime DateCreation { get; set; }
}