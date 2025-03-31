using MediLaboSolutions.Common.Interfaces;

namespace MediLaboSolutions.API.DTO
{
    public class PatientDTO : IPatient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Genre { get; set; }
        public string? AdressePostale { get; set; }
        public int? Telephone { get; set; }
    }
}