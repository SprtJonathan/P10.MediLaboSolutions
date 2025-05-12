using MediLaboSolutions.Common.Enumerables;

namespace MediLaboSolutions.Common.Interfaces
{
    public interface IPatient
    {
        string Nom { get; set; }
        string Prenom { get; set; }
        DateTime DateNaissance { get; set; }
        EPatientGender Genre { get; set; }
        int? AdresseId { get; set; }
        long? Telephone { get; set; }
    }
}
