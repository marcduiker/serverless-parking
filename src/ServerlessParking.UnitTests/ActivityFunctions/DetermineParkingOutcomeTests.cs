using FakeItEasy;
using FluentAssertions;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.ActivityFunctions;
using ServerlessParking.Models;
using Xunit;

namespace ServerlessParking.UnitTests.ActivityFunctions
{    
    public class DetermineParkingOutcomeTests
    {
        [Trait("Activity", nameof(DetermineParkingOutcome))]
        [Fact]
        public void GivenTheCarIsNeitherAnAppointmentOrEmployee_WhenParkingOutcomeIsDetermined_ThenGateOpenShouldBeFalse()
        {
            // Arrange
            var input = GetInputForCarIsNeitherAppointmentOrEmployee();
            var fakeTraceWriter = A.Fake<TraceWriter>();

            // Act
            DetermineParkingOutcomeResult result = DetermineParkingOutcome.DoWork(input, fakeTraceWriter);

            // Assert
            result.ParkingClientResult.GateOpen.Should().Be(false);
        }

        [Trait("Activity", nameof(DetermineParkingOutcome))]
        [Fact]
        public void GivenTheCarIsAnAppointmentAndSpaceIsAvailable_WhenParkingOutcomeIsDetermined_ThenGateOpenShouldBeTrue()
        {
            // Arrange
            var input = GetInputForCarIsAppointmentAndSpaceIsAvailable();
            var fakeTraceWriter = A.Fake<TraceWriter>();

            // Act
            DetermineParkingOutcomeResult result = DetermineParkingOutcome.DoWork(input, fakeTraceWriter);

            // Assert
            result.ParkingClientResult.GateOpen.Should().Be(true);
        }

        [Trait("Activity", nameof(DetermineParkingOutcome))]
        [Fact]
        public void GivenTheCarIsEmployeeAndSpaceIsAvailable_WhenParkingOutcomeIsDetermined_ThenGateOpenShouldBeTrue()
        {
            // Arrange
            var input = GetInputForCarIsEmployeeAndSpaceIsAvailable();
            var fakeTraceWriter = A.Fake<TraceWriter>();

            // Act
            DetermineParkingOutcomeResult result = DetermineParkingOutcome.DoWork(input, fakeTraceWriter);

            // Assert
            result.ParkingClientResult.GateOpen.Should().Be(true);
        }

        [Trait("Activity", nameof(DetermineParkingOutcome))]
        [Fact]
        public void GivenTheCarNeitherEmployeeOrAppointmentAndSpaceIsAvailable_WhenParkingOutcomeIsDetermined_ThenGateOpenShouldBeFalse()
        {
            // Arrange
            var input = GetInputForCarIsNeitherEmployeeOrAppointmentAndSpaceIsAvailable();
            var fakeTraceWriter = A.Fake<TraceWriter>();

            // Act
            DetermineParkingOutcomeResult result = DetermineParkingOutcome.DoWork(input, fakeTraceWriter);

            // Assert
            result.ParkingClientResult.GateOpen.Should().Be(false);
        }

        private static DetermineParkingInput GetInputForCarIsNeitherAppointmentOrEmployee()
        {
            return new DetermineParkingInput
            {
                IsAppointment = new ActivityResult { Result = false },
                IsEmployee = new ActivityResult { Result = false }
            };
        }

        private static DetermineParkingInput GetInputForCarIsAppointmentAndSpaceIsAvailable()
        {
            return new DetermineParkingInput
            {
                IsAppointment = new ActivityResult { Result = true },
                IsEmployee = new ActivityResult { Result = false },
                IsParkingSpotAvailable = new ActivityResult { Result = true }
            };
        }

        private static DetermineParkingInput GetInputForCarIsEmployeeAndSpaceIsAvailable()
        {
            return new DetermineParkingInput
            {
                IsAppointment = new ActivityResult { Result = false },
                IsEmployee = new ActivityResult { Result = true },
                IsParkingSpotAvailable = new ActivityResult { Result = true }
            };
        }

        private static DetermineParkingInput GetInputForCarIsNeitherEmployeeOrAppointmentAndSpaceIsAvailable()
        {
            return new DetermineParkingInput
            {
                IsAppointment = new ActivityResult { Result = false },
                IsEmployee = new ActivityResult { Result = false },
                IsParkingSpotAvailable = new ActivityResult { Result = true }
            };
        }
    }
}
