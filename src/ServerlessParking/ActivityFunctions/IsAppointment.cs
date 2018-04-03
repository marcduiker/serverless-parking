using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class IsAppointment
    {
        [FunctionName("IsAppointment")]
        public static ActivityResult Run(
            [ActivityTrigger] DurableActivityContext activityContext,
            TraceWriter log)
        {
            string licensePlate = activityContext.GetInput<string>();
            log.Info($"Checking license plate {licensePlate}.");

            if (Appointments.TryGetValue(licensePlate, out string appointmentName))
            {
                return new ActivityResult
                {
                    Name = appointmentName,
                    Result = true
                };
            }

            return new ActivityResult
            {
                Result = false
            };
        }

        private static readonly Dictionary<string, string> Appointments = new Dictionary<string, string>
        {
            { "XYZ-999", "Spiderman"},
            { "XXX-666", "Deadpool"}
        };
    }


}
