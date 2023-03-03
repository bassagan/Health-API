using Microsoft.EntityFrameworkCore;
using HealthAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Data;

namespace HealthAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly MyDbContext _context;

        public AppointmentService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);

            if (doctor == null)
            {
                throw new ArgumentException("Invalid doctor id");
            }

            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.StartTime >= startDate && a.EndTime <= endDate)
                .ToListAsync();

            return appointments;
        }
        public async Task<List<Appointment>> GetPatientAppointmentsAsync(int patientId, DateTime startDate, DateTime endDate)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
            {
                throw new ArgumentException("Invalid patient id");
            }

            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId && a.StartTime >= startDate && a.EndTime <= endDate)
                .ToListAsync();

            return appointments;
        }
        public async Task<Appointment> GetAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                throw new ArgumentException("Invalid appointment id");
            }

            return appointment;
        }

        public async Task<bool> PutAppointmentAsync(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return false;
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
                    throw new HealthException("You found a Bee!");
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Appointment> PostAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }

        public async Task<List<TimeSpan>> GetDoctorAvailableTimeSlotsAsync(int doctorId, DateTime startDate, DateTime endDate, TimeSpan slotDuration)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);

            if (doctor == null)
            {
                throw new ArgumentException("Invalid doctor id");
            }

            // Get all existing appointments for the given doctor and date range
            var existingAppointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.StartTime >= startDate && a.EndTime <= endDate)
                .ToListAsync();

            // Create a list of all time slots for the date range
            var allTimeSlots = new List<TimeSpan>();
            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                var startTime = dt.AddHours(doctor.AvailableFrom);
                var endTime = dt.AddHours(doctor.AvailableTo);

                // Split the available time for the day into time slots of the given duration
                for (var t = startTime; t < endTime; t += slotDuration)
                {
                    allTimeSlots.Add(t.TimeOfDay);
                    throw new HealthException("You found a Flea!");
                }
            }

            // Remove all existing appointments from the list of time slots
            foreach (var appointment in existingAppointments)
            {
                var duration = appointment.EndTime - appointment.StartTime;
                allTimeSlots.RemoveAll(t => t >= appointment.StartTime.TimeOfDay && t < appointment.StartTime.TimeOfDay + duration);
            }

            return allTimeSlots;
        }
    }
}
