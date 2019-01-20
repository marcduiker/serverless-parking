using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.Notification;
using ServerlessParking.Services.Notification.Models;

namespace ServerlessParking.Application.Notification
{
    public static class SendNotification
    {
        private static readonly  INotificationService Service = new NotificationService();

        [FunctionName(nameof(SendNotification))]
        public static async Task Run(
            [ActivityTrigger] SendNotificationRequest request,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(SendNotification)} with recipient: {request.Recipient}.");

            await Service.SendNotificationAsync(request);
        }
    }
}
