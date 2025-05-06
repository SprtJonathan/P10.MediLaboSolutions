using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Common.Interfaces;

namespace MediLaboSolutions.API.Dto
{
    public class PatientDto : IPatient
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public EPatientGender Genre { get; set; }
        public string? AdressePostale { get; set; }
        public long? Telephone { get; set; }
    }
}