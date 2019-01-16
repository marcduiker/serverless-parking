using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;
using ServerlessParking.Storage;
using ServerlessParking.Storage.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ServerlessParking.ActivityFunctions
{
    public static class IsEmployee
    {
        private static IParkingStorageClient _parkingStorageClient;
        public static IParkingStorageClient ParkingStorageClient
        {
            get { return _parkingStorageClient = _parkingStorageClient ?? new ParkingStorageClient(); }
            set => _parkingStorageClient = value;
        }

        [FunctionName(nameof(IsEmployee))]
        public static async Task<ActivityResult> Run(
            [ActivityTrigger] string licensePlate,
            ILogger log)
        {
            log.LogInformation($"Checking employee registration for license plate {licensePlate}.");

            ActivityResult activityResult = new ActivityResult { Result = false };
            try
            {
                var employee = await ParkingStorageClient.RetrieveEntity<Employee>(
                    Environment.GetEnvironmentVariable("LicenseplateRegistrationsTableName"),
                    nameof(Employee),
                    licensePlate);
                if (!string.IsNullOrEmpty(employee?.LicensePlate))
                {
                    activityResult.Result = true;
                    activityResult.Name = employee?.Name;
                }
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
            }

            return activityResult;
        }
    }
}
