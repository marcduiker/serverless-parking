namespace ServerlessParking.Services.ParkingGarageGate.Models
{
    public class DisplayMessageRequest
    {
        public DisplayMessageRequest(
            string parkingGarageName,
            string message)
        {
            ParkingGarageName = parkingGarageName;
            Message = message;
        }

        public string ParkingGarageName { get; set; }

        public string Message { get; set; }
    }
}
