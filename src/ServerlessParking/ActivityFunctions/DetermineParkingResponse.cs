using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class DetermineParkingResponse
    {
        [FunctionName("DetermineParkingResponse")]
        public static DetermineParkingResult Run(
            [ActivityTrigger] DurableActivityContext activityContext,
            TraceWriter log)
        {
            var parkingInput = activityContext.GetInput<DetermineParkingInput>();
            log.Info($"Determining parking response for {parkingInput.LicensePlate}.");
            var functionResult = new DetermineParkingResult();

            if (!parkingInput.IsAppointment.Result && !parkingInput.IsEmployee.Result)
            {
                // Car is neither an employee nor a guest
                // Respond to client that they can't park here.
                functionResult.ClientResult.Message = "This car is not registered to be parked here.";
                functionResult.ClientResult.Result = false;

                return functionResult;
            }

            if (!parkingInput.IsParkingSpotAvailable.Result)
            {
                functionResult.ClientResult.Result = parkingInput.IsParkingSpotAvailable.Result;
                if (parkingInput.IsAppointment.Result && !parkingInput.IsEmployee.Result)
                {
                    // Car is guest with an appointment and there is no parking spot.
                    // Send a message to the reception to help the guest.
                    // Respond to client that there is no parking space and reception is informed.
                    functionResult.ActivityFunction = "SendMessageToReception";
                    functionResult.ActivityData = $"Guest {parkingInput.IsAppointment.Name} has arrived but there is no parking spot.";
                    functionResult.ClientResult.Message = $"Welcome {parkingInput.IsAppointment.Name}, there is currently no parking spot available but we've contacted the reception to help you out.";
                }
                else if (!parkingInput.IsAppointment.Result && parkingInput.IsEmployee.Result)
                {
                    // Car is employee and there is no parking space.
                    // Respond to client that there is no parking space.
                    functionResult.ClientResult.Message = "Sorry, no parking spot available! :(";
                }
            }
            else
            {
                functionResult.ClientResult.Result = parkingInput.IsParkingSpotAvailable.Result;
                if (parkingInput.IsAppointment.Result && !parkingInput.IsEmployee.Result)
                {
                    // Car is guest with appointment and there is a parking spot.
                    // Send a message to reception that a guest has arrived.
                    // Respond to client that there is a parking spot.
                    functionResult.ActivityFunction = "SendMessageToReception";
                    functionResult.ActivityData = $"Guest {parkingInput.IsAppointment.Name} has arrived.";
                    functionResult.ClientResult.Message = $"Welcome {parkingInput.IsAppointment.Name}, you can park at spot {parkingInput.IsParkingSpotAvailable.Name}.";
                }
                else if (!parkingInput.IsAppointment.Result && parkingInput.IsEmployee.Result)
                {
                    // Car is employee and there is a parking spot.
                    // Respond to client that there is a parking spot.
                    functionResult.ClientResult.Message = $"Hi {parkingInput.IsEmployee.Name}, we have a parking spot with your name on it! :)";
                }
            }
           
            return functionResult;
        }
    }
}
