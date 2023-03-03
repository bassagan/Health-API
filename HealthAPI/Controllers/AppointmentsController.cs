using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Data;
using HealthAPI.Services;

namespace HealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: api/AppointmentPlanner/Doctors/5/Appointments
        [HttpGet("Doctors/{id}/Appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorAppointments(int id, DateTime startDate, DateTime endDate)
        {
            var appointments = await _appointmentService.GetDoctorAppointmentsAsync(id, startDate, endDate);

            if (!appointments.Any())
            {
                return NotFound();
            }

            return appointments;
        }

        // POST: api/AppointmentPlanner/Appointments
        [HttpPost("Appointments")]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            await _appointmentService.PostAppointmentAsync(appointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // GET: api/AppointmentPlanner/Appointments/5
        [HttpGet("Appointments/{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = new Appointment(); 

            try
            {
                appointment = await _appointmentService.GetAppointmentAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Avengers found a Dragon fly!" });
            }
            return appointment;
        }

        // PUT: api/AppointmentPlanner/Appointments/5
        [HttpPut("Appointments/{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            var success = await _appointmentService.PutAppointmentAsync(id, appointment);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/AppointmentPlanner/Appointments/5
        [HttpDelete("Appointments/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var success = await _appointmentService.DeleteAppointmentAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
