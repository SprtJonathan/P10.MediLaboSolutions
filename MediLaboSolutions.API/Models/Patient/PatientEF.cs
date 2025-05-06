using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.API.Models.Patient
{
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

        /// <summary>
        /// Nom du patient
        /// </summary>
        [Required]
        public string Nom { get; set; }

        /// <summary>
        /// Prénom du patient
        /// </summary>
        [Required]
        public string Prenom { get; set; }

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
        /// Adresse postale du patient
        /// </summary>
        public string? AdressePostale { get; set; }

        /// <summary>
        /// Numéro de téléphone du patient
        /// </summary>
        [MaxLength(10, ErrorMessage = "Le numéro de téléphone ne doit pas dépasser 10 chiffres.")]
        public long? Telephone { get; set; }
    }
}