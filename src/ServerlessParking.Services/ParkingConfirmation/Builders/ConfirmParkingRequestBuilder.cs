using System;
using ServerlessParking.Services.ParkingConfirmation.Models;

namespace ServerlessParking.Services.ParkingConfirmation.Builders
{
    public static class ConfirmParkingRequestBuilder
    {
        public static ConfirmParkingRequest Build(string parkingGarageName, Domain.LicensePlateRegistration licensePlateRegistration)
        {
            return new ConfirmParkingRequest(parkingGarageName, licensePlateRegistration); 
        }
    }
}
