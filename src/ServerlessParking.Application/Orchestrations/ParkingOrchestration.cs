using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using ServerlessParking.Application.Gate;
using ServerlessParking.Application.LicensePlate;
using ServerlessParking.Application.ConfirmParking.Models;
using ServerlessParking.Application.Notification;
using ServerlessParking.Application.Orchestrations.Models;
using ServerlessParking.Domain;
using ServerlessParking.Application.ConfirmParking;

namespace ServerlessParking.Application.Orchestrations
{
    public static class ParkingOrchestration
    {
        [FunctionName(nameof(ParkingOrchestration))]
        public static async Task<ParkingOrchestrationResponse> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContextBase context)
        {
            var request = context.GetInput<ParkingOrchestrationRequest>();

            var licensePlateResult = await context.CallActivityAsync<Domain.LicensePlate>(nameof(GetLicensePlate), request.LicensePlateNumber);

            Task<ConfirmParkingResponse> confirmTask;
            var confirmParkingRequest = new ConfirmParkingRequest(request.ParkingGarageName, licensePlateResult);

            switch (licensePlateResult.Type)
            {
                case LicensePlateType.Appointment:
                    confirmTask = context.CallActivityAsync<ConfirmParkingResponse>(nameof(ConfirmParkingForAppointment), confirmParkingRequest);
                    break;
                case LicensePlateType.Employee:
                    confirmTask = context.CallActivityAsync<ConfirmParkingResponse>(nameof(ConfirmParkingForEmployee), confirmParkingRequest);
                    break;
                default:
                    var unknownLicencePlateResponse = new ConfirmParkingResponse(request.ParkingGarageName, false);
                    confirmTask = Task.FromResult(unknownLicencePlateResponse);
                    break;
            }

            var parkingConfirmationResponse = await confirmTask;
            if (parkingConfirmationResponse.IsSuccess)
            {
                // TODO create request and do something with the result
                await context.CallActivityAsync<string>(nameof(OpenGate), parkingConfirmationResponse);
            }
            else
            {
                // TODO create request and do something with the result
                await context.CallActivityAsync<string>(nameof(SendNotificationtoContact), confirmParkingRequest);
            }

            var parkingOrchestrationResponse = new ParkingOrchestrationResponse();

            return parkingOrchestrationResponse;
        }
    }
}