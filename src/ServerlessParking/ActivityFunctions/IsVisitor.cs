using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;
using ServerlessParking.Storage;
using ServerlessParking.Storage.Entities;

namespace ServerlessParking.ActivityFunctions
{
    public static class IsVisitor
    {
        private static IParkingStorageClient _parkingStorageClient;
        public static IParkingStorageClient ParkingStorageClient
        {
            get { return _parkingStorageClient = _parkingStorageClient ?? new ParkingStorageClient(); }
            set => _parkingStorageClient = value;
        }

        [FunctionName(nameof(IsVisitor))]
        public static async Task<ActivityResult> Run(
            [ActivityTrigger] string licensePlate,
            TraceWriter log)
        {
            log.Info($"Checking visitor registration for license plate {licensePlate}.");

            ActivityResult activityResult = new ActivityResult { Result = false };
            try
            {
                var visitor = await ParkingStorageClient.RetrieveEntity<Visitor>(
                    Environment.GetEnvironmentVariable("LicenseplateRegistrationsTableName"),
                    nameof(Visitor),
                    licensePlate);
                if (!string.IsNullOrEmpty(visitor?.LicensePlate))
                {
                    activityResult.Result = true;
                    activityResult.Name = $"{visitor?.Name} ({visitor?.Company})";
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }

            return activityResult;
        }
    }
}
