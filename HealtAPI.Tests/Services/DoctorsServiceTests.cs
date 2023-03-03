using HealthAPI.Data;
using HealthAPI.Model;
using HealthAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace HealthAPI.Tests.Services
{
    public class DoctorsServiceTests
    {

        private readonly Mock<MyDbContext> _mockContext;
        private readonly DoctorsService _service;

        public DoctorsServiceTests()
        {

            _mockContext = new Mock<MyDbContext>(new DbContextOptionsBuilder<MyDbContext>()
                            .UseInMemoryDatabase(databaseName: (string)"test_database")
                            .Options) { CallBase = true };

            _service = new DoctorsService(_mockContext.Object);
        }
        [Fact]
        public async Task GetDoctorsAsync_ReturnsOnlyDoctorsWithPatients()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDoctorsAsync_ReturnsOnlyDoctorsWithPatients")
                .Options;

            using (var context = new MyDbContext(options))
            {
                var service = new DoctorsService(context);

                var patient1 = new Patient { Id = 1, Name = "Patient 1" };
                var patient2 = new Patient { Id = 2, Name = "Patient 2" };

                var doctor1 = new Doctor { Id = 1, Name = "Doctor 1", Speciality = "Cardiologist", AvailableFrom = 34, AvailableTo = 55, Patients = new List<Patient>() {patient1, patient2 } };
                var doctor2 = new Doctor { Id = 2, Name = "Doctor 2", Speciality = "Cardiologist", AvailableFrom = 34, AvailableTo = 55, Patients = new List<Patient>() { patient1, patient2 } };
                var doctor3 = new Doctor { Id = 3, Name = "Doctor 3", Speciality = "Cardiologist", AvailableFrom = 34, AvailableTo = 55, Patients = new List<Patient>() { patient1, patient2 } };

             

                context.Doctors.AddRange(new List<Doctor> { doctor1, doctor2, doctor3 });
                context.Patients.AddRange(new List<Patient> { patient1, patient2 });
                await context.SaveChangesAsync();

                // Act
                var result = await service.GetDoctorsAsync();

                // Assert
                Assert.Equal(3, result.Count());
            }
        }
    }
}