using MediLaboSolutions.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.Web.Models.Patients
{
    public class AdresseDto : IAdresse
    {
        public int Id { get; set; }

        [Display(Name = "Numéro")]
        public int? Numero { get; set; }

        [Display(Name = "Voie")]
        public string? Voie { get; set; }

        [Display(Name = "Ville")]
        public string? Ville { get; set; }

        [Display(Name = "Code postal")]
        public string? CodePostal { get; set; }

        [Display(Name = "Pays")]
        public string? Pays { get; set; }
    }
}
