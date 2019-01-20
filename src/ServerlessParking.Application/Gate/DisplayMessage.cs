using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.ParkingGarageGate;

namespace ServerlessParking.Application.Gate
{
    public static class DisplayMessage
    {
        private static readonly IParkingGarageGateService Service = new ParkingGarageGateService();

        [FunctionName(nameof(DisplayMessage))]
        public static async Task Run(
            [ActivityTrigger] string message,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(DisplayMessage)}.");

            await Service.DisplayMessage(message);
        }
    }
}
