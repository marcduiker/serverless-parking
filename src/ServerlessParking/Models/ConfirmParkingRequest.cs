namespace ServerlessParking.Application.Models
{
    public class ConfirmParkingRequest
    {
        public ConfirmParkingRequest(string parkingGarageName, Domain.LicensePlate licensePlate)
        {
            ParkingGarageName = parkingGarageName;
            LicensePlate = licensePlate;
        }

        public string ParkingGarageName { get; set; }

        public Domain.LicensePlate LicensePlate { get; set; }
    }
}
