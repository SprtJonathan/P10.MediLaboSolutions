namespace MediLaboSolutions.Common.Interfaces
{
    public interface INote
    {
        int PatientId { get; set; }
        string Nom { get; set; }
        string Prenom { get; set; }
        string PraticienUsername { get; set; }
        string Texte { get; set; }
        DateTime DateCreation { get; set; }
    }
}
