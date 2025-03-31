using MediLaboSolutions.Common.Interfaces;
using MediLaboSolutions.Web.Models.Patients;
using MediLaboSolutions.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly PatientService _patientService;

        // Injection de PatientService via le constructeur
        public PatientsController(PatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        // GET: PatientsController
        public async Task<ActionResult> Index()
        {
            // Récupération de la liste des patients de manière asynchrone
            var patients = await _patientService.GetAllPatientsAsync();

            // Passage de la liste des patients à la vue
            return View(patients);
        }

        // GET: PatientsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientDto patientDto)
        {
            // Vérification de la validité des données
            if (!ModelState.IsValid)
            {
                return View(patientDto); // Retourne la vue avec les erreurs
            }

            try
            {
                // Ajout du patient de manière asynchrone
                await _patientService.AddPatientAsync(patientDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Gestion des erreurs (vous pouvez logger l'exception si nécessaire)
                ModelState.AddModelError("", "Une erreur est survenue lors de la création du patient.");
                return View(patientDto);
            }
        }

        // GET: PatientsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
