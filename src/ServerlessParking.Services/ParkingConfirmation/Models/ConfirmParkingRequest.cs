using ServerlessParking.Domain;

namespace ServerlessParking.Services.ParkingConfirmation.Models
{
    public class ConfirmParkingRequest
    {
        public ConfirmParkingRequest(
            string parkingGarageName, 
            LicensePlateRegistration licensePlateRegistration)
        {
            ParkingGarageName = parkingGarageName;
            LicensePlateRegistration = licensePlateRegistration;
        }

        public string ParkingGarageName { get; set; }

        public LicensePlateRegistration LicensePlateRegistration { get; set; }
    }
}
