using Microsoft.EntityFrameworkCore;
using HealthAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Data;

namespace HealthAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly MyDbContext _context;

        public PatientService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<bool> PutPatientAsync(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return false;
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return false;
                }
                else
                {
                    throw new HealthException("You found a Ladybug!");
                }
            }

            return true;
        }

        public async Task<Patient> PostPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
