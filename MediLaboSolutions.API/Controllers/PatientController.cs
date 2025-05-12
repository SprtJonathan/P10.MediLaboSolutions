using MediLaboSolutions.API.Dto;
using MediLaboSolutions.API.Models.Patient;
using MediLaboSolutions.API.Repositories;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var patients = await _repository.GetAllAsync();
            var dtos = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Prenom = p.Prenom,
                DateNaissance = p.DateNaissance,
                Genre = p.Genre,
                Telephone = p.Telephone,
                AdresseId = p.AdresseId,
                Adresse = p.Adresse == null ? null : new AdresseDto
                {
                    Id = p.Adresse.Id,
                    Numero = p.Adresse.Numero,
                    Voie = p.Adresse.Voie,
                    Ville = p.Adresse.Ville,
                    CodePostal = p.Adresse.CodePostal,
                    Pays = p.Adresse.Pays
                }
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return NotFound();

            var dto = new PatientDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Prenom = p.Prenom,
                DateNaissance = p.DateNaissance,
                Genre = p.Genre,
                Telephone = p.Telephone,
                AdresseId = p.AdresseId,
                Adresse = p.Adresse == null ? null : new AdresseDto
                {
                    Id = p.Adresse.Id,
                    Numero = p.Adresse.Numero,
                    Voie = p.Adresse.Voie,
                    Ville = p.Adresse.Ville,
                    CodePostal = p.Adresse.CodePostal,
                    Pays = p.Adresse.Pays
                }
            };
            return Ok(dto);
        }

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
                Telephone = patientDto.Telephone,
                Adresse = patientDto.Adresse == null ? null : new AdresseEF
                {
                    Numero = patientDto.Adresse.Numero,
                    Voie = patientDto.Adresse.Voie,
                    Ville = patientDto.Adresse.Ville,
                    CodePostal = patientDto.Adresse.CodePostal,
                    Pays = patientDto.Adresse.Pays
                }
            };

            await _repository.AddAsync(patient);
            patientDto.Id = patient.Id;
            patientDto.AdresseId = patient.AdresseId;

            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patientDto);
        }

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
            patient.Telephone = patientDto.Telephone;

            if (patientDto.Adresse != null)
            {
                patient.Adresse = new AdresseEF
                {
                    Numero = patientDto.Adresse.Numero,
                    Voie = patientDto.Adresse.Voie,
                    Ville = patientDto.Adresse.Ville,
                    CodePostal = patientDto.Adresse.CodePostal,
                    Pays = patientDto.Adresse.Pays
                };
            }
            else
            {
                patient.Adresse = null;
                patient.AdresseId = null;
            }


            await _repository.UpdateAsync(patient);
            return NoContent();
        }

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
