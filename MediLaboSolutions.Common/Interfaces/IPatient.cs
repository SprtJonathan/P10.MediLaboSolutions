namespace MediLaboSolutions.Common.Interfaces
{
    public interface IPatient
    {
        string Nom { get; set; }
        string Prenom { get; set; }
        DateTime DateNaissance { get; set; }
        string Genre { get; set; }
        string? AdressePostale { get; set; }
        int? Telephone { get; set; }
    }
}
