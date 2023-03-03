using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthAPI.Model;
using HealthAPI.Services;

namespace HealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorService;

        public DoctorsController(IDoctorsService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await _doctorService.GetDoctorsAsync();
            return Ok(doctors);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            var updatedDoctor = await _doctorService.UpdateDoctorAsync(id, doctor);

            if (updatedDoctor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Avengers you found a Fly!" });
            }

            return Ok(updatedDoctor);
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            var createdDoctor = new Doctor();
            try
            {
                createdDoctor = await _doctorService.AddDoctorAsync(doctor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Avengers you found a Buterfly!" });

            }


            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.Id }, createdDoctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _doctorService.DeleteDoctorAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}