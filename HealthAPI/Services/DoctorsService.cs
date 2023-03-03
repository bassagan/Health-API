using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthAPI.Data;
using HealthAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly MyDbContext _context;

        public DoctorsService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            // Only return doctors that have at least one patient
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.Patients)
                .SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> UpdateDoctorAsync(int id, Doctor doctor)
        {
            // Only allow updating a doctor's email address
            var doctorToUpdate = await _context.Doctors.FindAsync(id);
            if (doctorToUpdate == null)
            {
                return false;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return doctor;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            // Only allow deleting a doctor if they have no patients
            var doctorToDelete = await _context.Doctors.Include(d => d.Patients).FirstOrDefaultAsync(d => d.Id == id);
            if (doctorToDelete == null)
            {
                return false;
            }

            if (doctorToDelete.Patients.Any())
            {
                return false;
            }

            _context.Doctors.Remove(doctorToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}