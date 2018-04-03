using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using ServerlessParking.Models;

namespace ServerlessParking
{
    public static class ParkingOrchestration
    {
        [FunctionName("ParkingOrchestration")]
        public static async Task<ParkingClientResult> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext orchestrationContext)
        {
            var licensePlate = orchestrationContext.GetInput<string>();

            var isParkingSpotAvailableActivity = orchestrationContext.CallActivityAsync<ActivityResult>("IsParkingSpotAvailable");
            var isEmployeeActivity = orchestrationContext.CallActivityAsync<ActivityResult>("IsEmployee", licensePlate);
            var isAppointmentActivity = orchestrationContext.CallActivityAsync<ActivityResult>("IsAppointment", licensePlate);

            // Replace "hello" with the name of your Durable Activity Function.
            var activities = new List<Task<ActivityResult>>
            {
                isParkingSpotAvailableActivity,
                isEmployeeActivity,
                isAppointmentActivity
            };

            await Task.WhenAll(activities);
            
            var determineParkingInput = new DetermineParkingInput
            {
                IsParkingSpotAvailable = isParkingSpotAvailableActivity.Result,
                IsEmployee = isEmployeeActivity.Result,
                IsAppointment = isAppointmentActivity.Result
            };

            var determineParkingResult = await orchestrationContext.CallActivityAsync<DetermineParkingResult>(
                "DetermineParkingResponse",
                determineParkingInput);


            return determineParkingResult.ClientResult;
        }
    }
}