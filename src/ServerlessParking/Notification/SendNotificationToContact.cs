using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Build.Framework;

namespace ServerlessParking.Application.Notification
{
    public static class SendNotificationtoContact
    {
        [FunctionName(nameof(SendNotificationtoContact))]
        public static Task Run(
            [ActivityTrigger] string parkingGarageName,
            ILogger logger)
        {
            // This sends a request to open the gate of the parking garage.

            return Task.CompletedTask;
        }
    }
}
