using MediLaboSolutions.Common.Interfaces;

namespace MediLaboSolutions.API.Dto
{
    public class AdresseDto : IAdresse
    {
        public int Id { get; set; }
        public int? Numero { get; set; }
        public string? Voie { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }
        public string? Pays { get; set; }
    }
}
