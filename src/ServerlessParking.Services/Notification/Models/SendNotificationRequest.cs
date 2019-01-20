namespace ServerlessParking.Services.Notification.Models
{
    public class SendNotificationRequest
    {
        public string Recipient { get; set; }

        public string Message { get; set; }
    }
}
