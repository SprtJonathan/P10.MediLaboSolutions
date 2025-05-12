using MediLaboSolutions.Web.Models.Patients;
using MediLaboSolutions.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.Web.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly PatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(PatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _logger = logger;
        }

        // GET: PatientsController
        public async Task<ActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Récupération de la liste des patients pour la vue Index");
                var patients = await _patientService.GetAllPatientsAsync();
                _logger.LogInformation("Liste des patients récupérée avec succès");
                return View(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de la liste des patients");
                ModelState.AddModelError("", "Une erreur est survenue lors de la récupération des patients.");
                return View(new List<PatientDto>());
            }
        }

        // GET: PatientsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                _logger.LogInformation($"Récupération des détails du patient avec l'ID {id}");
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning($"Aucun patient trouvé avec l'ID {id}");
                    return NotFound();
                }
                _logger.LogInformation($"Détails du patient avec l'ID {id} récupérés avec succès");
                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération des détails du patient avec l'ID {id}");
                ModelState.AddModelError("", "Une erreur est survenue lors de la récupération des détails du patient.");
                return View();
            }
        }

        // GET: PatientsController/Create
        public ActionResult Create()
        {
            _logger.LogInformation("Affichage de la vue pour créer un nouveau patient");
            return View();
        }

        // POST: PatientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Données invalides lors de la création d'un patient");
                return View(patientDto);
            }

            try
            {
                _logger.LogInformation("Tentative d'ajout d'un nouveau patient");

                var json = System.Text.Json.JsonSerializer.Serialize(patientDto);
                _logger.LogInformation("JSON envoyé : {Json}", json);

                await _patientService.AddPatientAsync(patientDto);
                _logger.LogInformation("Patient ajouté avec succès");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du patient");
                ModelState.AddModelError("", "Une erreur est survenue lors de la création du patient.");
                return View(patientDto);
            }
        }

        // GET: PatientsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                _logger.LogInformation($"Récupération du patient avec l'ID {id} pour modification");
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning($"Aucun patient trouvé avec l'ID {id}");
                    return NotFound();
                }
                _logger.LogInformation($"Patient avec l'ID {id} récupéré pour modification");
                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération du patient avec l'ID {id} pour modification");
                ModelState.AddModelError("", "Une erreur est survenue lors de la récupération du patient.");
                return View();
            }
        }

        // POST: PatientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Données invalides lors de la modification du patient avec l'ID {id}");
                return View(patientDto);
            }

            try
            {
                _logger.LogInformation($"Tentative de mise à jour du patient avec l'ID {id}");

                var json = System.Text.Json.JsonSerializer.Serialize(patientDto);
                _logger.LogInformation("JSON envoyé : {Json}", json);

                await _patientService.UpdatePatientAsync(id, patientDto);
                _logger.LogInformation($"Patient avec l'ID {id} mis à jour avec succès");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la mise à jour du patient avec l'ID {id}");
                ModelState.AddModelError("", "Une erreur est survenue lors de la mise à jour du patient.");
                return View(patientDto);
            }
        }

        // GET: PatientsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Récupération du patient avec l'ID {id} pour suppression");
                var patient = await _patientService.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning($"Aucun patient trouvé avec l'ID {id}");
                    return NotFound();
                }
                _logger.LogInformation($"Patient avec l'ID {id} récupéré pour confirmation de suppression");
                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération du patient avec l'ID {id} pour suppression");
                ModelState.AddModelError("", "Une erreur est survenue lors de la récupération du patient.");
                return View();
            }
        }

        // POST: PatientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                _logger.LogInformation($"Tentative de suppression du patient avec l'ID {id}");
                await _patientService.DeletePatientAsync(id);
                _logger.LogInformation($"Patient avec l'ID {id} supprimé avec succès");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression du patient avec l'ID {id}");
                ModelState.AddModelError("", "Une erreur est survenue lors de la suppression du patient.");
                return View();
            }
        }
    }
}