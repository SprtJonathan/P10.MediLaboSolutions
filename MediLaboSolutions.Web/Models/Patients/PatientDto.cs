using MediLaboSolutions.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.Web.Models.Patients
{
    public class PatientDto : IPatient
    {
        /// <summary>
        /// Id du patient
        /// </summary>
        [Required]
        public string? Id { get; set; }

        /// <summary>
        /// Nom du patient
        /// </summary>
        [Required]
        public string? Nom { get; set; }

        /// <summary>
        /// Prénom du patient
        /// </summary>
        [Required]
        public string? Prenom { get; set; }

        /// <summary>
        /// Date de naissance du patient
        /// </summary>
        [Required]
        public DateTime DateNaissance { get; set; }

        /// <summary>
        /// Genre du patient
        /// </summary>
        [Required]
        public string? Genre { get; set; }

        /// <summary>
        /// Adresse postale du patient
        /// </summary>
        public string? AdressePostale { get; set; }

        /// <summary>
        /// Numéro de téléphone du patient
        /// </summary>
        public int? Telephone { get; set; }
    }
}
