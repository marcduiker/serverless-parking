using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class IsParkingSpotAvailable
    {
        [FunctionName(nameof(IsParkingSpotAvailable))]
        public static ActivityResult Run(
            [ActivityTrigger] DurableActivityContext activityContext,
            TraceWriter log)
        {
            log.Info($"Checking parking availability.");

            if (ParkingSpotsWithAvailability.ContainsValue(true))
            {
                int parkingSpot = ParkingSpotsWithAvailability.FirstOrDefault(spot => spot.Value).Key;
                return new ActivityResult
                {
                    Result = true,
                    Name = parkingSpot.ToString()
                };
            }

            return new ActivityResult
            {
                Result = false
            };
        }

        private static readonly Dictionary<int, bool> ParkingSpotsWithAvailability = new Dictionary<int, bool>
        {
            { 1, false},
            { 2, false},
            { 3, true}
        };
    }
}
