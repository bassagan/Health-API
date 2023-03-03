using HealthAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthAPI.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientAsync(int id);
        Task<bool> PutPatientAsync(int id, Patient patient);
        Task<Patient> PostPatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
    }
}