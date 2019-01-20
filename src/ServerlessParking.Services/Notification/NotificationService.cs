using System.Threading.Tasks;
using ServerlessParking.Services.Notification.Models;

namespace ServerlessParking.Services.Notification
{
    public class NotificationService : INotificationService
    {
        public async Task SendNotificationAsync(SendNotificationRequest request)
        {
            // Simulate successfull sending of a notification.
            await Task.CompletedTask;
        }
    }
}
