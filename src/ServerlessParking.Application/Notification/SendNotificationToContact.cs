using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.Notification;
using ServerlessParking.Services.Notification.Models;

namespace ServerlessParking.Application.Notification
{
    public static class SendNotificationtoContact
    {
        private static readonly  INotificationService Service = new NotificationService();

        [FunctionName(nameof(SendNotificationtoContact))]
        public static async Task Run(
            [ActivityTrigger] SendNotificationRequest request,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(SendNotificationtoContact)} with recipient: {request.Recipient}.");

            await Service.SendNotificationAsync(request);
        }
    }
}
