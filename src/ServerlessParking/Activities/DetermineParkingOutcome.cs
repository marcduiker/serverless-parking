using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class DetermineParkingOutcome
    {
        [FunctionName(nameof(DetermineParkingOutcome))]
        public static DetermineParkingOutcomeResult Run(
            [ActivityTrigger] DetermineParkingInput parkingInput,
            ILogger log)
        {
            log.LogInformation($"Determining parking response for {parkingInput.LicensePlate}.");
            var functionResult = new DetermineParkingOutcomeResult();

            if (!parkingInput.IsAppointment.Result && !parkingInput.IsEmployee.Result)
            {
                // Car is neither an employee nor a guest
                // Respond to client that they can't park here.
                functionResult.ParkingClientResult.Message = "This car is not registered to be parked here.";
                functionResult.ParkingClientResult.GateOpen = false;

                return functionResult;
            }

            if (!parkingInput.IsParkingSpotAvailable.Result)
            {
                functionResult.ParkingClientResult.GateOpen = parkingInput.IsParkingSpotAvailable.Result;
                if (parkingInput.IsAppointment.Result && !parkingInput.IsEmployee.Result)
                {
                    // Car is guest with an appointment and there is no parking spot.
                    // Send a message to the reception to help the guest.
                    // Respond to client that there is no parking space and reception is informed.
                    functionResult.ActivityFunction = nameof(SendMessage);
                    functionResult.ActivityData = new SendMessageInput
                    {
                        Recipient = "reception",
                        Message = $"Guest {parkingInput.IsAppointment.Name} has arrived but there is no parking spot."
                    };
                    functionResult.ParkingClientResult.Message = $"Welcome {parkingInput.IsAppointment.Name}, there is currently no parking spot available but we've contacted the reception to help you out.";
                }
                else if (!parkingInput.IsAppointment.Result && parkingInput.IsEmployee.Result)
                {
                    // Car is employee and there is no parking space.
                    // Respond to client that there is no parking space.
                    functionResult.ParkingClientResult.Message = "Sorry, no parking spot available! :(";
                }
            }
            else
            {
                functionResult.ParkingClientResult.GateOpen = parkingInput.IsParkingSpotAvailable.Result;
                if (parkingInput.IsAppointment.Result && !parkingInput.IsEmployee.Result)
                {
                    // Car is guest with appointment and there is a parking spot.
                    // Send a message to reception that a guest has arrived.
                    // Respond to client that there is a parking spot.
                    functionResult.ActivityFunction = nameof(SendMessage);
                    functionResult.ActivityData = new SendMessageInput
                    {
                        Recipient = "reception",
                        Message = $"Guest {parkingInput.IsAppointment.Name} has arrived."
                    };
                    functionResult.ParkingClientResult.Message = $"Welcome {parkingInput.IsAppointment.Name}, you can park at spot {parkingInput.IsParkingSpotAvailable.Name}.";
                }
                else if (!parkingInput.IsAppointment.Result && parkingInput.IsEmployee.Result)
                {
                    // Car is employee and there is a parking spot.
                    // Respond to client that there is a parking spot.
                    functionResult.ParkingClientResult.Message = $"Hi {parkingInput.IsEmployee.Name}, we have a parking spot with your name on it! :)";
                }
            }

            return functionResult;
        }
    }
}
