using MediLaboSolutions.Common.Enumerables;

namespace MediLaboSolutions.Evaluation.Models;

public class PatientDto
{
    public int? Id { get; set; }
    public DateTime DateNaissance { get; set; }
    public EPatientGender Genre { get; set; }
}

