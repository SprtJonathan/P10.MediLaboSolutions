using MediLaboSolutions.API.Dto;
using MediLaboSolutions.API.Models.Patient;
using MediLaboSolutions.API.Repositories;
using MediLaboSolutions.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientsController(IPatientRepository repository)
        {
            _repository = repository;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var patients = await _repository.GetAllAsync();
            var patientDtos = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Prenom = p.Prenom,
                DateNaissance = p.DateNaissance,
                Genre = p.Genre,
                AdressePostale = p.AdressePostale,
                Telephone = p.Telephone
            });
            return Ok(patientDtos);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return NotFound();
            var patientDto = new PatientDto
            {
                Id = patient.Id,
                Nom = patient.Nom,
                Prenom = patient.Prenom,
                DateNaissance = patient.DateNaissance,
                Genre = patient.Genre,
                AdressePostale = patient.AdressePostale,
                Telephone = patient.Telephone
            };
            return Ok(patientDto);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create([FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var patient = new PatientEF
            {
                Nom = patientDto.Nom,
                Prenom = patientDto.Prenom,
                DateNaissance = patientDto.DateNaissance,
                Genre = patientDto.Genre,
                AdressePostale = patientDto.AdressePostale,
                Telephone = patientDto.Telephone
            };

            await _repository.AddAsync(patient);
            patientDto.Id = patient.Id; // Récupère l'ID généré
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patientDto);
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto patientDto)
        {
            if (id != patientDto.Id) return BadRequest();
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return NotFound();

            patient.Nom = patientDto.Nom;
            patient.Prenom = patientDto.Prenom;
            patient.DateNaissance = patientDto.DateNaissance;
            patient.Genre = patientDto.Genre;
            patient.AdressePostale = patientDto.AdressePostale;
            patient.Telephone = patientDto.Telephone;

            await _repository.UpdateAsync(patient);
            return NoContent();
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}