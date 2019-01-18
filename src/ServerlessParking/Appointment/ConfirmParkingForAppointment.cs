using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Application.Models;
using ServerlessParking.Interfaces;
using ServerlessParking.Repositories;

namespace ServerlessParking.Application.Appointment
{
    public static class ConfirmParkingForAppointment
    {
        private static readonly IParkingGarageRepository Repository = new ParkingGarageRepository();

        [FunctionName(nameof(ConfirmParkingForAppointment))]
        public static async Task<ConfirmParkingResponse> Run(
            [ActivityTrigger] ConfirmParkingRequest request,
            ILogger log)
        {
            log.LogInformation($"Started {nameof(ConfirmParkingForAppointment)} for licensePlate {request.LicensePlate.Number}.");

            var parkingGarage = await Repository.FindByNameAndDateAsync(request.ParkingGarageName, DateTime.Today);
            var occupySpaceResult = parkingGarage.OccupyParkingSpace(withReservation: true);

            return new ConfirmParkingResponse(parkingGarage.Name, occupySpaceResult);
        }
    }
}
