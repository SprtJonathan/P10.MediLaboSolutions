using MediLaboSolutions.Common.Interfaces;
using System.Diagnostics;

namespace MediLaboSolutions.Notes.Models.Note;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class NoteEF : BaseModelEntityNoSql, INote
{
    public NoteEF() : base()
    {
    }

    public NoteEF(string id) : base(id)
    {
    }

    protected override string DebuggerDisplay
        => $"{base.DebuggerDisplay}, Patient = {PatientId}, Date = {DateCreation}";

    public int PatientId { get; set; }
    public required string PraticienUsername { get; set; }
    public required string Texte { get; set; }
    public DateTime DateCreation { get; set; }
}

