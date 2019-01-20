using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using ServerlessParking.Application.Gate;
using ServerlessParking.Application.LicensePlate;
using ServerlessParking.Application.Notification;
using ServerlessParking.Application.Orchestrations.Models;
using ServerlessParking.Domain;
using ServerlessParking.Application.ConfirmParking;
using ServerlessParking.Application.Orchestrations.Builders;
using ServerlessParking.Services.Notification.Builders;
using ServerlessParking.Services.ParkingConfirmation.Builders;
using ServerlessParking.Services.ParkingConfirmation.Models;

namespace ServerlessParking.Application.Orchestrations
{
    public static class ParkingOrchestration
    {
        [FunctionName(nameof(ParkingOrchestration))]
        public static async Task<ParkingOrchestrationResponse> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContextBase context)
        {
            var request = context.GetInput<ParkingOrchestrationRequest>();

            var licensePlateResult = await context.CallActivityAsync<Domain.LicensePlateRegistration>(nameof(GetLicensePlate), request.LicensePlateNumber);

            Task<ConfirmParkingResponse> confirmTask;
            var confirmParkingRequest = ConfirmParkingRequestBuilder.Build(request.ParkingGarageName, licensePlateResult);

            switch (licensePlateResult.Type)
            {
                case LicensePlateType.Appointment:
                    confirmTask = context.CallActivityAsync<ConfirmParkingResponse>(nameof(ConfirmParkingForAppointment), confirmParkingRequest);
                    break;
                case LicensePlateType.Employee:
                    confirmTask = context.CallActivityAsync<ConfirmParkingResponse>(nameof(ConfirmParkingForEmployee), confirmParkingRequest);
                    break;
                default:
                    var unknownLicencePlateResponse =  ConfirmParkingResponseBuilder.BuildWithFailedUnknownLicensePlate(request.ParkingGarageName);
                    confirmTask = Task.FromResult(unknownLicencePlateResponse);
                    break;
            }

            var parkingConfirmationResponse = await confirmTask;

            if (parkingConfirmationResponse.IsSuccess)
            {
                await context.CallActivityAsync(nameof(OpenGate), confirmTask.Result.ParkingGarageName);
            }
            else
            {
                await context.CallActivityAsync(nameof(DisplayMessage), confirmTask.Result.Message);
            }

            if (licensePlateResult.Type == LicensePlateType.Appointment)
            {
                var sendNotificationRequest = SendNotificationRequestBuilder.BuildWithAppointmentHasArrived(
                    licensePlateResult.ContactPerson,
                    licensePlateResult.Name);
                await context.CallActivityAsync(nameof(SendNotificationtoContact), sendNotificationRequest);
            }


            var parkingOrchestrationResponse = ParkingOrchestrationResponseBuilder.Build(
                licensePlateResult, 
                confirmTask.Result.IsSuccess);

            return parkingOrchestrationResponse;
        }
    }
}