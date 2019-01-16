using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class SendMessage
    {
        [FunctionName(nameof(SendMessage))]
        public static void Run(
            [ActivityTrigger] SendMessageInput input,
            ILogger log)
        {
            log.LogInformation($"Send message to {input.Recipient}.");

            // Do a fire & forget message to the recipient
        }
    }
}
