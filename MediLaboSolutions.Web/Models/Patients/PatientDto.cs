using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.Web.Models.Patients
{
    public class PatientDto : IPatient
    {
        /// <summary>
        /// Id du patient
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nom du patient
        /// </summary>
        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom doit comporter entre 2 et 50 caractères.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\-]+$", ErrorMessage = "Le nom ne peut contenir que des lettres et des tirets.")]
        [Display(Name = "Nom")]
        public required string Nom { get; set; }

        /// <summary>
        /// Prénom du patient
        /// </summary>
        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit comporter entre 2 et 50 caractères.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\-]+$", ErrorMessage = "Le prénom ne peut contenir que des lettres et des tirets.")]
        [Display(Name = "Prénom")]
        public required string Prenom { get; set; }

        /// <summary>
        /// Date de naissance du patient
        /// </summary>
        [Required(ErrorMessage = "La date de naissance est requise.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public required DateTime DateNaissance { get; set; }

        /// <summary>
        /// Genre du patient
        /// </summary>
        [Required]
        [Display(Name = "Genre du patient")]
        public required EPatientGender Genre { get; set; }

        /// <summary>
        /// Adresse postale du patient
        /// </summary>
        [Display(Name = "Adresse postale")]
        public int? AdresseId { get; set; }

        /// <summary>
        /// Numéro de téléphone du patient
        /// </summary>
        [Display(Name = "Téléphone")]
        public long? Telephone { get; set; }

        public virtual AdresseDto? Adresse { get; set; }
    }
}
