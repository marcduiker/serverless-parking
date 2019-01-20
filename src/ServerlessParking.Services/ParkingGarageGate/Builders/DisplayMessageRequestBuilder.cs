using ServerlessParking.Services.ParkingGarageGate.Models;

namespace ServerlessParking.Services.ParkingGarageGate.Builders
{
    public static class DisplayMessageRequestBuilder
    {
        public static DisplayMessageRequest Build(string parkingGarageName, string message)
        {
            return new DisplayMessageRequest(parkingGarageName, message);
        }
    }
}
