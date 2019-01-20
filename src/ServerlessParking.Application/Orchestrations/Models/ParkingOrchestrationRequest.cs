namespace ServerlessParking.Application.Orchestrations.Models
{
    public class ParkingOrchestrationRequest
    {
        public string LicensePlateNumber { get; set; }

        public string ParkingGarageName { get; set; }
    }
}
