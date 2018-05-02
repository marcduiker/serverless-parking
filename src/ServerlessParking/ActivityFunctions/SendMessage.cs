using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ServerlessParking.Models;

namespace ServerlessParking.ActivityFunctions
{
    public static class SendMessage
    {
        [FunctionName(nameof(SendMessage))]
        public static void Run(
            [ActivityTrigger] SendMessageInput input,
            TraceWriter log)
        {
            log.Info($"Send message to {input.Recipient}.");

            // Do a fire & forget message to the recipient
        }
    }
}
