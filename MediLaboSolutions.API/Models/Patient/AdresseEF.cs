using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Common.Interfaces;
using MediLaboSolutions.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MediLaboSolutions.API.Models.Patient
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdresseEF : BaseModelEntity, IAdresse
    {
        // Constructeur par défaut pour initialiser les propriétés obligatoires
        public AdresseEF() : base()
        {
        }

        // Constructeur avec ID
        public AdresseEF(int id) : base(id)
        {
        }

        protected override string DebuggerDisplay
            => $"{base.DebuggerDisplay}, Adresse = {Voie}";

        /// <summary>
        /// Numéro de voie
        /// </summary>
        public int? Numero { get; set; }

        /// <summary>
        /// Nom de la voie
        /// </summary>
        public string? Voie { get; set; }

        /// <summary>
        /// Ville
        /// </summary>
        public string? Ville { get; set; }

        /// <summary>
        /// Code postal
        /// </summary>
        public string? CodePostal { get; set; }

        /// <summary>
        /// Pays
        /// </summary>
        public string? Pays { get; set; }
    }
}