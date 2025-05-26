using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Evaluation.Models;
using Microsoft.Extensions.Logging;

namespace MediLaboSolutions.Evaluation.Services
{
    public class AssessmentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AssessmentService> _logger;

        private static readonly string[] _triggerStems = new[]
        {
            "hémoglobine a1c",
            "microalbumine",
            "taille",
            "poids",
            "fum",         // pour fume, fumer, fumeur, fumeuse
            "anormal",     // anormal, anormale
            "cholestérol",
            "vertig",      // vertige, vertiges
            "rechute",
            "réaction",
            "anticorps"
        };

        public AssessmentService(IHttpClientFactory clientFactory, ILogger<AssessmentService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<ENiveauRisque> EvaluerRisqueAsync(int patientId)
        {
            var client = _clientFactory.CreateClient();

            var patient = await client.GetFromJsonAsync<PatientDto>($"https://localhost:7157/api/patients/{patientId}");
            var notes = await client.GetFromJsonAsync<List<NoteDto>>($"https://localhost:7157/api/notes");

            if (patient == null || notes == null)
            {
                _logger.LogWarning("Patient {patientId} ou notes introuvables, retourne NONE", patientId);
                return ENiveauRisque.None;
            }

            // Récupération du texte de toutes les notes du patient
            var allText = notes
                .Where(n => n.PatientId == patientId)
                .Select(n => n.Texte)
                .Aggregate(new StringBuilder(), (sb, t) => sb.Append(' ').Append(t))
                .ToString()
                .ToLowerInvariant();

            // On matche chaque stem + tout suffixe (\w*)
            var matched = _triggerStems
                .Where(stem => Regex.IsMatch(allText,
                    $@"\b{Regex.Escape(stem)}\w*\b",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                .Distinct()
                .ToList();

            var count = matched.Count;
            _logger.LogInformation("Patient {patientId} : triggers détectés = [{triggers}], total={count}",
                patientId,
                string.Join(", ", matched),
                count);

            // Calcul de l'âge exact
            var age = DateTime.UtcNow.Year - patient.DateNaissance.Year;
            if (patient.DateNaissance > DateTime.UtcNow.AddYears(-age)) age--;

            // Délégation à la méthode de décision
            var risk = DetermineRiskLevel(count, age, patient.Genre);
            _logger.LogInformation("Patient {patientId} : age={age}, genre={genre} => risque {risk}",
                patientId, age, patient.Genre, risk);

            return risk;
        }

        /// <summary>
        /// Détermine le niveau de risque du patient selon les facteurs trouvés
        /// </summary>
        /// <param name="count"></param>
        /// <param name="age"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        private static ENiveauRisque DetermineRiskLevel(int count, int age, EPatientGender genre)
        {
            if (count == 0)
                return ENiveauRisque.None;

            // Plus de 30 ans
            if (age > 30)
            {
                if (count >= 8) return ENiveauRisque.EarlyOnset;
                if (count >= 6) return ENiveauRisque.InDanger;
                if (count >= 2) return ENiveauRisque.Borderline;
            }
            // Moins ou égal à 30 ans
            else
            {
                if (genre == EPatientGender.Homme)
                {
                    if (count >= 5) return ENiveauRisque.EarlyOnset;
                    if (count >= 3) return ENiveauRisque.InDanger;
                }
                else // Femme ou autre
                {
                    if (count >= 7) return ENiveauRisque.EarlyOnset;
                    if (count >= 4) return ENiveauRisque.InDanger;
                }
            }

            // Par défaut, aucun risque
            return ENiveauRisque.None;
        }
    }
}
