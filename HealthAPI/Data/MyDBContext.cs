using HealthAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; internal set; }
    }
}
