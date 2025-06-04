using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Text;
using FuzzySharp;
using MediLaboSolutions.Common.Enumerables;
using MediLaboSolutions.Evaluation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediLaboSolutions.Evaluation.Services
{
    public class AssessmentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AssessmentService> _logger;

        private static readonly Dictionary<string, string[]> _triggerGroups = new()
        {
            ["hémoglobine"] = new[] { "hémoglobine a1c", "hba1c" },
            ["microalbumine"] = new[] { "microalbumine" },
            ["taille"] = new[] { "taille" },
            ["poids"] = new[] { "poids" },
            ["tabac"] = new[] { "fum", "fumer", "fume", "fumeur", "fumeuse", "tabac", "cigarette" },
            ["anormal"] = new[] { "anormal", "anormale" },
            ["cholestérol"] = new[] { "cholestérol", "cholesterol" },
            ["vertige"] = new[] { "vertige", "vertiges" },
            ["rechute"] = new[] { "rechute" },
            ["réaction"] = new[] { "réaction", "réactions" },
            ["anticorps"] = new[] { "anticorps" }
        };

        public AssessmentService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, ILogger<AssessmentService> logger)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ENiveauRisque> EvaluerRisqueAsync(int patientId)
        {
            var client = _clientFactory.CreateClient();

            // Récupérer le token JWT de la requête entrante
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                // Ajouter le token aux requêtes sortantes
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _logger.LogWarning("Aucun token JWT trouvé dans la requête entrante pour patientId={patientId}", patientId);
                return ENiveauRisque.None; // Ou gérer autrement selon les besoins
            }

            var patient = await client.GetFromJsonAsync<PatientDto>($"https://localhost:7157/api/patients/{patientId}");
            var notes = await client.GetFromJsonAsync<List<NoteDto>>($"https://localhost:7157/api/notes");

            if (patient is null || notes is null)
            {
                _logger.LogWarning("Patient {patientId} ou notes introuvables, retourne NONE", patientId);
                return ENiveauRisque.None;
            }

            var fullNoteText = string.Join(" ", notes
                .Where(n => n.PatientId == patientId)
                .Select(n => n.Texte))
                .ToLowerInvariant();

            var distinctWords = Regex.Matches(fullNoteText, @"\b\w+\b")
                                     .Select(m => m.Value)
                                     .Distinct()
                                     .ToList();

            var matchedGroups = DetectTriggerGroups(fullNoteText, distinctWords);

            _logger.LogInformation("Patient {patientId} : groupes détectés = [{triggers}], total={count}",
                patientId,
                string.Join(", ", matchedGroups),
                matchedGroups.Count);

            var age = CalculateAge(patient.DateNaissance);

            var risk = DetermineRiskLevel(matchedGroups.Count, age, patient.Genre);

            _logger.LogInformation("Patient {patientId} : age={age}, genre={genre} => risque {risk}",
                patientId, age, patient.Genre, risk);

            return risk;
        }

        /// <summary>
        /// Détecte les groupes déclencheurs présents dans le texte, avec tolérance aux fautes.
        /// </summary>
        private List<string> DetectTriggerGroups(string text, List<string> distinctWords)
        {
            var matchedGroups = new List<string>();

            foreach (var (groupKey, variants) in _triggerGroups)
            {
                // 1. Regex stricte
                if (variants.Any(variant =>
                        Regex.IsMatch(text, $@"\b{Regex.Escape(variant)}\w*\b", RegexOptions.IgnoreCase)))
                {
                    matchedGroups.Add(groupKey);
                    continue;
                }

                // 2. Fallback fuzzy, mais filtré
                foreach (var variant in variants)
                {
                    var best = Process.ExtractOne(variant, distinctWords
                        .Where(w => w.Length >= 4) // ignore les mots trop courts
                        .ToList());

                    if (best != null && best.Score >= 92)
                    {
                        _logger.LogDebug("Fuzzy match pour groupe '{group}' via '{variant}' avec le mot '{word}' (score {score})",
                            groupKey, variant, best.Value, best.Score);
                        matchedGroups.Add(groupKey);
                        break;
                    }
                }
            }

            return matchedGroups;
        }

        /// <summary>
        /// Calcule l'âge du patient à partir de sa date de naissance.
        /// </summary>
        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Détermine le niveau de risque du patient selon les facteurs trouvés.
        /// </summary>
        private static ENiveauRisque DetermineRiskLevel(int count, int age, EPatientGender genre)
        {
            if (count == 0)
                return ENiveauRisque.None;

            if (age > 30)
            {
                if (count >= 8) return ENiveauRisque.EarlyOnset;
                if (count >= 6) return ENiveauRisque.InDanger;
                if (count >= 2 && count <= 5) return ENiveauRisque.Borderline;
            }
            else // âge ≤ 30
            {
                if (genre == EPatientGender.Homme)
                {
                    if (count >= 5) return ENiveauRisque.EarlyOnset;
                    if (count >= 3 && count <= 4) return ENiveauRisque.InDanger;
                }
                else // Femme ou autre
                {
                    if (count >= 7) return ENiveauRisque.EarlyOnset;
                    if (count >= 4 && count <= 6) return ENiveauRisque.InDanger;
                }
            }

            return ENiveauRisque.None;
        }
    }
}