
namespace HealthAPI.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int? DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}