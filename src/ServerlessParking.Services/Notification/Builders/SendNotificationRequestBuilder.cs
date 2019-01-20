using ServerlessParking.Services.Notification.Models;

namespace ServerlessParking.Services.Notification.Builders
{
    public static class SendNotificationRequestBuilder
    {
        public static SendNotificationRequest BuildWithAppointmentHasArrived(string recipient, string appointmentName)
        {
            return new SendNotificationRequest(recipient, $"Your appointment, {appointmentName}, has arrived.");
        }
    }
}
