using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.ActivityFunctions;
using ServerlessParking.Storage;
using ServerlessParking.Storage.Entities;
using Xunit;

namespace ServerlessParking.UnitTests.ActivityFunctions
{
    public class IsEmployeeTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Trait("Activity", nameof(IsEmployee))]
        [Fact]
        public void GivenTheLicensePlateIsRegisteredForAnEmployee_WhenTheIsEmployeeMethodIsRun_ThenTheActivityResultShouldBeTrue()
        {
            // Arrange
            string licensePlate = _fixture.Create<string>();
            IsEmployee.ParkingStorageClient = GetFakeParkingStorageWhichReturnsAnEmployee(licensePlate);
            var fakeTraceWriter = A.Fake<TraceWriter>();

            // Act
            var task = IsEmployee.Run(licensePlate, fakeTraceWriter);

            // Assert
            task.Result.Result.Should().Be(true);
        }

        [Trait("Activity", nameof(IsEmployee))]
        [Fact]
        public void GivenTheLicensePlateIsNotRegisteredForAnEmployee_WhenTheIsEmployeeMethodIsRun_ThenTheActivityResultShouldBeFalse()
        {
            // Arrange
            string licensePlate = _fixture.Create<string>();
            IsEmployee.ParkingStorageClient = GetFakeParkingStorageWhichReturnsANullEmployee();
            var fakeTraceWriter = A.Fake<TraceWriter>();

            // Act
            var task = IsEmployee.Run(licensePlate, fakeTraceWriter);

            // Assert
            task.Result.Result.Should().Be(false);
        }

        private static IParkingStorageClient GetFakeParkingStorageWhichReturnsAnEmployee(string licensePlate)
        {
            return GetFakeParkingStorageClient(new Employee("Employee", licensePlate));
        }

        private static IParkingStorageClient GetFakeParkingStorageWhichReturnsANullEmployee()
        {
            return GetFakeParkingStorageClient(null);
        }

        private static IParkingStorageClient GetFakeParkingStorageClient(Employee desiredOutput)
        {
            var fake = A.Fake<IParkingStorageClient>();
            A.CallTo(() => fake.RetrieveEntity<Employee>(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .ReturnsLazily(() => desiredOutput);

            return fake;
        }
    }
}
