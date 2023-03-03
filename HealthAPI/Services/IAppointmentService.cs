using HealthAPI.Model;

namespace HealthAPI.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<Appointment> GetAppointmentAsync(int id);
        Task<bool> PutAppointmentAsync(int id, Appointment appointment);
        Task<Appointment> PostAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}