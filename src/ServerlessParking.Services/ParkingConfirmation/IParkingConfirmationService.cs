using System;
using System.Threading.Tasks;
using ServerlessParking.Services.ParkingConfirmation.Models;

namespace ServerlessParking.Services.ParkingConfirmation
{
    public interface IParkingConfirmationService
    {
        Task<ConfirmParkingResponse> ConfirmParkingAsync(
            ConfirmParkingRequest request, 
            DateTime parkingDate, 
            bool hasReservation);
    }
}
