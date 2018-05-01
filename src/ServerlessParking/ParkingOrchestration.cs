using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using ServerlessParking.ActivityFunctions;
using ServerlessParking.Models;

namespace ServerlessParking
{
    public static class ParkingOrchestration
    {
        [FunctionName(nameof(ParkingOrchestration))]
        public static async Task<ParkingClientResult> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext orchestrationContext)
        {
            var licensePlate = orchestrationContext.GetInput<string>();

            var isParkingSpotAvailableActivity = orchestrationContext.CallActivityAsync<ActivityResult>(nameof(IsParkingSpotAvailable), null);
            var isEmployeeActivity = orchestrationContext.CallActivityAsync<ActivityResult>(nameof(IsEmployee), licensePlate);
            var isAppointmentActivity = orchestrationContext.CallActivityAsync<ActivityResult>(nameof(IsAppointment), licensePlate);

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

            var determineParkingResult = await orchestrationContext.CallActivityAsync<DetermineParkingOutcomeResult>(
                nameof(DetermineParkingOutcome),
                determineParkingInput);


            return determineParkingResult.ParkingClientResult;
        }
    }
}