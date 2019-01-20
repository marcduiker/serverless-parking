using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.ParkingGarageGate;

namespace ServerlessParking.Application.Gate
{
    public static class OpenGate
    {
        private static readonly IParkingGarageGateService Service = new ParkingGarageGateService();

        [FunctionName(nameof(OpenGate))]
        public static async Task Run(
            [ActivityTrigger] string parkingGarageName,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(OpenGate)}.");

            await Service.OpenGateAsync(parkingGarageName);
        }
    }
}
