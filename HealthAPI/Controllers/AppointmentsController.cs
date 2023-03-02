using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Data;

namespace HealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AppointmentsController(MyDbContext context)
        {
            _context = context;
        }

        

        // GET: api/AppointmentPlanner/Doctors/5/Appointments
        [HttpGet("Doctors/{id}/Appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorAppointments(int id, DateTime startDate, DateTime endDate)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            var patient = await _context.Patients.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => patient.Id)
                .Where(a => a.DoctorId == id && a.StartTime >= startDate && a.EndTime <= endDate)
                .ToListAsync();

            return appointments;
        }


        // POST: api/AppointmentPlanner/Appointments
        [HttpPost("Appointments")]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // GET: api/AppointmentPlanner/Appointments/5
        [HttpGet("Appointments/{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
           

            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
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

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/AppointmentPlanner/Appointments/5
        [HttpDelete("Appointments/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}

