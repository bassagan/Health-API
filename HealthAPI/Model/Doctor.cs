namespace HealthAPI.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public List<Patient> Patients { get; set; }
        public double AvailableFrom { get; set; }
        public double AvailableTo { get; set; }
    }
}
