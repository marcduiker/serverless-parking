using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Application.ConfirmParking;
using ServerlessParking.Application.Gate;
using ServerlessParking.Application.LicensePlate;
using ServerlessParking.Application.Orchestrations;
using ServerlessParking.Application.Orchestrations.Models;
using ServerlessParking.Domain;
using ServerlessParking.Services.ParkingConfirmation.Builders;
using ServerlessParking.Services.ParkingConfirmation.Models;
using ServerlessParking.Services.ParkingGarageGate.Models;
using Xunit;

namespace ServerlessParking.Application.UnitTests.Orchestrations
{
    public class ParkingGarageCarEntryOrchestrationTests
    {
        [Fact]
        public async Task GivenLicensePlateIsUnknown_WhenOrchestrationIsStarted_ThenGateOpenedShouldBeFalse()
        {
            // Arrange
            var context = CreateFakeContextForUnkownLicensePlate();
            var logger = A.Fake<ILogger>();

            // Act
            var result  = await ParkingGarageCarEntryOrchestration.Run(context, logger);

            // Assert
            result.GateOpened.Should().BeFalse();
        }

        [Fact]
        public async Task GivenLicensePlateBelongsToAppointment_WhenOrchestrationIsStarted_ThenGateOpenedShouldBeTrue()
        {
            // Arrange
            var context = CreateFakeContextForAppointment();
            var logger = A.Fake<ILogger>();

            // Act
            var result = await ParkingGarageCarEntryOrchestration.Run(context, logger);

            // Assert
            result.GateOpened.Should().BeTrue();
        }

        [Fact]
        public async Task GivenLicensePlateBelongsToEmployee_WhenOrchestrationIsStarted_ThenGateOpenedShouldBeTrue()
        {
            // Arrange
            var context = CreateFakeContextForEmployee();
            var logger = A.Fake<ILogger>();

            // Act
            var result = await ParkingGarageCarEntryOrchestration.Run(context, logger);

            // Assert
            result.GateOpened.Should().BeTrue();
        }

        private DurableOrchestrationContextBase CreateFakeContextForUnkownLicensePlate()
        {
            var context = A.Fake<DurableOrchestrationContextBase>();
            // Configure input
            A.CallTo(() => context.GetInput<ParkingOrchestrationRequest>())
                .Returns(new ParkingOrchestrationRequest
                {
                    ParkingGarageName = "Parking Garage 1",
                    LicensePlateNumber = "ABC-123"
                });

            // Configure GetLicensePlateRegistration activity
            A.CallTo(() => context.CallActivityAsync<LicensePlateRegistration>(
                    nameof(GetLicensePlateRegistration),
                    A<string>._))
                .Returns(Task.FromResult(new LicensePlateRegistration {Type = LicensePlateType.Unknown}));

            // Configure DisplayMessage activity
            A.CallTo(() => context.CallActivityAsync(
                    nameof(DisplayMessage),
                    A<DisplayMessageRequest>._))
                .Returns(Task.CompletedTask);

            return context;
        }

        private DurableOrchestrationContextBase CreateFakeContextForAppointment()
        {
            var context = A.Fake<DurableOrchestrationContextBase>();
            const string licensePlateNumber = "ABC-123";
            const string parkingGarageName = "Parking Garage 1";
            
            // Configure input
            A.CallTo(() => context.GetInput<ParkingOrchestrationRequest>())
                .Returns(new ParkingOrchestrationRequest
                {
                    ParkingGarageName = parkingGarageName,
                    LicensePlateNumber = licensePlateNumber
                });

            // Configure GetLicensePlateRegistration activity
            A.CallTo(() => context.CallActivityAsync<LicensePlateRegistration>(
                    nameof(GetLicensePlateRegistration),
                    A<string>._))
                .Returns(Task.FromResult(new LicensePlateRegistration
                {
                    ContactPerson = "C.O. NTact",
                    Number = licensePlateNumber,
                    Name = "A.P. Pointment",
                    Type = LicensePlateType.Appointment
                }));

            // Configure ConfirmParkingForAppointment activity
            A.CallTo(() => context.CallActivityAsync<ConfirmParkingResponse>(
                    nameof(ConfirmParkingForAppointment),
                    A<ConfirmParkingRequest>._))
                .Returns(Task.FromResult(
                    ConfirmParkingResponseBuilder.BuildWithSuccess(parkingGarageName)));

            // Configure ConfirmParkingForAppointment activity
            A.CallTo(() => context.CallActivityAsync(
                    nameof(OpenGate),
                    A<string>._))
                .Returns(Task.CompletedTask);

            return context;
        }

        private DurableOrchestrationContextBase CreateFakeContextForEmployee()
        {
            var context = A.Fake<DurableOrchestrationContextBase>();
            const string licensePlateNumber = "ABC-123";
            const string parkingGarageName = "Parking Garage 1";

            // Configure input
            A.CallTo(() => context.GetInput<ParkingOrchestrationRequest>())
                .Returns(new ParkingOrchestrationRequest
                {
                    ParkingGarageName = parkingGarageName,
                    LicensePlateNumber = licensePlateNumber
                });

            // Configure GetLicensePlateRegistration activity
            A.CallTo(() => context.CallActivityAsync<LicensePlateRegistration>(
                    nameof(GetLicensePlateRegistration),
                    A<string>._))
                .Returns(Task.FromResult(new LicensePlateRegistration
                {
                    Number = licensePlateNumber,
                    Name = "E.M. Ployee",
                    Type = LicensePlateType.Employee
                }));

            // Configure ConfirmParkingForAppointment activity
            A.CallTo(() => context.CallActivityAsync<ConfirmParkingResponse>(
                    nameof(ConfirmParkingForEmployee),
                    A<ConfirmParkingRequest>._))
                .Returns(Task.FromResult(
                    ConfirmParkingResponseBuilder.BuildWithSuccess(parkingGarageName)));

            // Configure ConfirmParkingForAppointment activity
            A.CallTo(() => context.CallActivityAsync(
                    nameof(OpenGate),
                    A<string>._))
                .Returns(Task.CompletedTask);

            return context;
        }
    }
}
