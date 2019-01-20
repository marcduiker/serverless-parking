using ServerlessParking.Services.ParkingConfirmation.Models;

namespace ServerlessParking.Services.ParkingConfirmation.Builders
{
    public static class ConfirmParkingResponseBuilder
    {
        public static ConfirmParkingResponse Build(string parkingGarageName, bool isSuccess)
        {
            return new ConfirmParkingResponse(
                parkingGarageName,
                isSuccess,
                string.Empty);
        }

        public static ConfirmParkingResponse BuildWithSuccess(string parkingGarageName)
        {
            return new ConfirmParkingResponse(
                parkingGarageName, 
                true,
                string.Empty); 
        }

        public static ConfirmParkingResponse BuildWithFailedNoParkingSpaceAvailable(string parkingGarageName)
        {
            return new ConfirmParkingResponse(
                parkingGarageName,
                false,
                "No available parking spaces.");
        }

        public static ConfirmParkingResponse BuildWithFailedUnknownLicensePlate(string parkingGarageName)
        {
            return new ConfirmParkingResponse(
                parkingGarageName, 
                false,
                "License plate is unkown.");
        }
    }
}
