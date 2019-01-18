using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Models;

namespace ServerlessParking.Application.Activities
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
