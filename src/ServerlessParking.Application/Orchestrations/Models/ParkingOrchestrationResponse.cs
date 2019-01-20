using ServerlessParking.Domain;

namespace ServerlessParking.Application.Orchestrations.Models
{
    public sealed class ParkingOrchestrationResponse
    {
        public ParkingOrchestrationResponse(
            LicensePlateRegistration licensePlateRegistration,
            bool gateOpened)
        {
            LicensePlateRegistration = licensePlateRegistration;
            GateOpened = gateOpened;
        }

        public Domain.LicensePlateRegistration LicensePlateRegistration { get; set; }

        public bool GateOpened { get; set; }
    }
}
