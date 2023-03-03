using System.Collections.Generic;
using System.Threading.Tasks;
using HealthAPI.Model;

namespace HealthAPI.Services
{
    public interface IDoctorsService
    {
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<bool> UpdateDoctorAsync(int id, Doctor doctor);
        Task<Doctor> AddDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
