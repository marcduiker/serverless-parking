using System.Threading.Tasks;
using ServerlessParking.Services.Notification.Models;

namespace ServerlessParking.Services.Notification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(SendNotificationRequest request);
    }
}
