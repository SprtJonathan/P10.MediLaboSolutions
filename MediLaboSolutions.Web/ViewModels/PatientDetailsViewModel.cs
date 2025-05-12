using MediLaboSolutions.Web.Models.Notes;
using MediLaboSolutions.Web.Models.Patients;

namespace MediLaboSolutions.Web.ViewModels;

public class PatientDetailsViewModel
{
    public required PatientDto Patient { get; set; }
    public List<NoteDto> Notes { get; set; }
    public NoteDto? NewNote { get; set; }
}
