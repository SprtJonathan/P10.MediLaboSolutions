using MediLaboSolutions.Common.Interfaces;

namespace MediLaboSolutions.API.Dto
{
    public class PatientDto : IPatient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Genre { get; set; }
        public string? AdressePostale { get; set; }
        public long? Telephone { get; set; }
    }
}