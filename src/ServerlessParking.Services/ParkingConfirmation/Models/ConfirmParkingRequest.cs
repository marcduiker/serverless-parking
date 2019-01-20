namespace ServerlessParking.Services.ParkingConfirmation.Models
{
    public class ConfirmParkingRequest
    {
        public ConfirmParkingRequest(string parkingGarageName, Domain.LicensePlateRegistration licensePlateRegistration)
        {
            ParkingGarageName = parkingGarageName;
            LicensePlateRegistration = licensePlateRegistration;
        }

        public string ParkingGarageName { get; set; }

        public Domain.LicensePlateRegistration LicensePlateRegistration { get; set; }
    }
}
