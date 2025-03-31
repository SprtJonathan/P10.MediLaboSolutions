using MediLaboSolutions.Data.Models;
using MediLaboSolutions.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediLaboSolutions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientsController(IPatientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await _repository.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetById(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Patient patient)
        {
            await _repository.AddAsync(patient);
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Patient patient)
        {
            if (id != patient.Id)
                return BadRequest();
            await _repository.UpdateAsync(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}