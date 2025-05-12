using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MediLaboSolutions.API.Models.Patient
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PatientEF : BaseModelEntity, IPatient
    {
        // Constructeur par défaut pour initialiser les propriétés obligatoires
        public PatientEF() : base()
        {
        }

        // Constructeur avec ID
        public PatientEF(int id) : base(id)
        {
        }

        protected override string DebuggerDisplay
            => $"{base.DebuggerDisplay}, Nom complet = {Nom} {Prenom}";

        /// <summary>
        /// Nom du patient
        /// </summary>
        [Required]
        public required string Nom { get; set; }

        /// <summary>
        /// Prénom du patient
        /// </summary>
        [Required]
        public required string Prenom { get; set; }

        /// <summary>
        /// Date de naissance du patient
        /// </summary>
        [Required]
        public DateTime DateNaissance { get; set; }

        /// <summary>
        /// Genre du patient
        /// </summary>
        [Required]
        public EPatientGender Genre { get; set; }

        /// <summary>
        /// Id de l'adresse postale du patient
        /// </summary>
        public int? AdresseId { get; set; }

        /// <summary>
        /// Numéro de téléphone du patient
        /// </summary>
        [MaxLength(10, ErrorMessage = "Le numéro de téléphone ne doit pas dépasser 10 chiffres.")]
        public long? Telephone { get; set; }

        public virtual AdresseEF? Adresse { get; set; }
    }
}