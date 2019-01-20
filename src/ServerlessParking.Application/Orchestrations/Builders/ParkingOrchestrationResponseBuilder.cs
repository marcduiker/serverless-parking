using ServerlessParking.Application.Orchestrations.Models;
using ServerlessParking.Domain;

namespace ServerlessParking.Application.Orchestrations.Builders
{
    public static class ParkingOrchestrationResponseBuilder
    {
        public static ParkingOrchestrationResponse Build(
            LicensePlateRegistration licensePlateRegistration,
            bool gateOpened)
        {
            return new ParkingOrchestrationResponse(licensePlateRegistration, gateOpened);
        }
    }
}
