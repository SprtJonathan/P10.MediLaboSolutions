using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.Data.Models
{
    public class Patient
    {
        // Constructeur par défaut pour initialiser les propriétés obligatoires
        public Patient()
        {
        }

        // Constructeur sans ID
        public Patient(string nom, string prenom, DateTime dateNaissance, string genre, string? adressePostale = null, int? telephone = null)
        {
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            Genre = genre;
            AdressePostale = adressePostale;
            Telephone = telephone;
        }

        /// <summary>
        /// Id du patient (généré automatiquement par MongoDB)
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }

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
        public string Genre { get; set; }

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