namespace MediLaboSolutions.Common.Interfaces
{
    public interface IAdresse
    {
        int? Numero { get; set; }
        string Voie { get; set; }
        string? Ville { get; set; }
        string? CodePostal { get; set; }
        string? Pays { get; set; }
    }
}
