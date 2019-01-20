using System;
using System.Threading.Tasks;
using ServerlessParking.Application.ConfirmParking.Models;
using ServerlessParking.Interfaces.Repositories;
using ServerlessParking.Repositories;

namespace ServerlessParking.Services.ParkingConfirmation
{
    public class ParkingConfirmationService : IParkingConfirmationService
    {
        private readonly IParkingGarageRepository _repository;

        public ParkingConfirmationService(IParkingGarageRepository repository = null)
        {
            _repository = repository ?? new ParkingGarageRepository();
        }

        public async Task<ConfirmParkingResponse> ConfirmParkingAsync(
            ConfirmParkingRequest request, 
            DateTime parkingDate, 
            bool hasReservation)
        {
            var parkingGarage = await _repository.FindByNameAndDateAsync(request.ParkingGarageName, parkingDate);
            var occupySpaceResult = parkingGarage.OccupyParkingSpace(hasReservation);

            return new ConfirmParkingResponse(parkingGarage.Name, occupySpaceResult);
        }

    }
}
