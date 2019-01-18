using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Build.Framework;

namespace ServerlessParking.Application.Gate
{
    public static class OpenGate
    {
        [FunctionName(nameof(OpenGate))]
        public static Task Run(
            [ActivityTrigger] string parkingGarageName,
            ILogger logger)
        {
            // This sends a request to open the gate of the parking garage.

            return Task.CompletedTask;
        }
    }
}
