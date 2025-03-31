namespace MediLaboSolutions.Data.Models
{
    public class Patient
    {
        /// <summary>
        /// Id du patient
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Nom du patient
        /// </summary>
        public required string Nom { get; set; }

        /// <summary>
        /// Prénom du patient
        /// </summary>
        public required string Prenom { get; set; }

        /// <summary>
        /// Date de naissance du patient
        /// </summary>
        public required DateTime DateNaissance { get; set; }

        /// <summary>
        /// Genre du patient
        /// </summary>
        public required string Genre { get; set; }

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
