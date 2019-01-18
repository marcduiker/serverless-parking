using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using ServerlessParking.Application.Appointment;
using ServerlessParking.Application.Employee;
using ServerlessParking.Application.Gate;
using ServerlessParking.Application.LicensePlate;
using ServerlessParking.Application.Models;
using ServerlessParking.Domain;

namespace ServerlessParking.Application
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
                await context.CallActivityAsync<string>(nameof(OpenGate), confirmParkingRequest);
            }
            else
            {
                
            }

            var parkingOrchestrationResponse = new ParkingOrchestrationResponse();

            return parkingOrchestrationResponse;
        }
    }
}