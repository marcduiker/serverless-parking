namespace ServerlessParking.Services.Notification.Models
{
    public class SendNotificationRequest
    {
        public SendNotificationRequest(
            string recipient,
            string message)
        {
            Recipient = recipient;
            Message = message;
        }

        public string Recipient { get; set; }

        public string Message { get; set; }
    }
}
