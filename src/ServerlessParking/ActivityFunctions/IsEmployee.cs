using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class IsEmployee
    {
        [FunctionName(nameof(IsEmployee))]
        public static ActivityResult Run(
            [ActivityTrigger] DurableActivityContext activityContext,
            TraceWriter log)
        {
            string licensePlate = activityContext.GetInput<string>();
            log.Info($"Checking license plate {licensePlate}.");

            if (LicensePlates.TryGetValue(licensePlate, out string employeeName))
            {
                return new ActivityResult
                {
                    Name = employeeName,
                    Result = true
                };
            }

            return new ActivityResult
            {
                Result = false
            };
        }

        private static readonly Dictionary<string, string> LicensePlates = new Dictionary<string, string>
        {
            { "ABC-111", "Grace"},
            { "DEF-222", "Jane"}
        };
    }
}
