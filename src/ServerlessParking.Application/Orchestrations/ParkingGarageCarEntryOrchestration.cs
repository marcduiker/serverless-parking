using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
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
using ServerlessParking.Services.ParkingGarageGate.Builders;

namespace ServerlessParking.Application.Orchestrations
{
    public static class ParkingGarageCarEntryOrchestration
    {
        [FunctionName(nameof(ParkingGarageCarEntryOrchestration))]
        public static async Task<ParkingOrchestrationResponse> Run(
            [OrchestrationTrigger] DurableOrchestrationContextBase context,
            ILogger logger)
        {
            if (!context.IsReplaying)
            {
                logger.LogInformation($"Started {nameof(ParkingGarageCarEntryOrchestration)} with InstanceId: {context.InstanceId}.");
            }

            var request = context.GetInput<ParkingOrchestrationRequest>();

            var licensePlateResult = await context.CallActivityAsync<LicensePlateRegistration>(
                nameof(GetLicensePlateRegistration), 
                request.LicensePlateNumber);

            
            var confirmParkingRequest = ConfirmParkingRequestBuilder.Build(request.ParkingGarageName, licensePlateResult);
            var confirmParkingResponse = await ConfirmParking(confirmParkingRequest, licensePlateResult, context);

            if (confirmParkingResponse.IsSuccess)
            {
                await context.CallActivityAsync(
                    nameof(OpenGate), 
                    confirmParkingResponse.ParkingGarageName);
            }
            else
            {
                var displayMessageRequest = DisplayMessageRequestBuilder.Build(
                    confirmParkingResponse.ParkingGarageName,
                    confirmParkingResponse.Message);
                await context.CallActivityAsync(
                    nameof(DisplayMessage), 
                    displayMessageRequest);
            }

            if (licensePlateResult.Type == LicensePlateType.Appointment)
            {
                var sendNotificationRequest = SendNotificationRequestBuilder.BuildWithAppointmentHasArrived(
                    licensePlateResult.ContactPerson,
                    licensePlateResult.Name);
                await context.CallActivityAsync(
                    nameof(SendNotification), 
                    sendNotificationRequest);
            }


            var parkingOrchestrationResponse = ParkingOrchestrationResponseBuilder.Build(
                licensePlateResult,
                confirmParkingResponse.IsSuccess);

            return parkingOrchestrationResponse;
        }

        private static Task<ConfirmParkingResponse> ConfirmParking(
            ConfirmParkingRequest request,
            LicensePlateRegistration licensePlateRegistration,
            DurableOrchestrationContextBase context)
        {
            Task<ConfirmParkingResponse> confirmTask;

            switch (licensePlateRegistration.Type)
            {
                case LicensePlateType.Appointment:
                    confirmTask = context.CallActivityAsync<ConfirmParkingResponse>(
                        nameof(ConfirmParkingForAppointment),
                        request);
                    break;
                case LicensePlateType.Employee:
                    confirmTask = context.CallActivityAsync<ConfirmParkingResponse>(
                        nameof(ConfirmParkingForEmployee),
                        request);
                    break;
                default:
                    var unknownLicencePlateResponse = ConfirmParkingResponseBuilder.BuildWithFailedUnknownLicensePlate(request.ParkingGarageName);
                    confirmTask = Task.FromResult(unknownLicencePlateResponse);
                    break;
            }

            return confirmTask;
        }
    }
}