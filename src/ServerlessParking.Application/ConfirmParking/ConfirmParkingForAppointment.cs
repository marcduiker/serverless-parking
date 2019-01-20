using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Application.ConfirmParking.Models;
using ServerlessParking.Services.ParkingConfirmation;

namespace ServerlessParking.Application.ConfirmParking
{
    public static class ConfirmParkingForAppointment
    {

        private static readonly IParkingConfirmationService Service = new ParkingConfirmationService();

        [FunctionName(nameof(ConfirmParkingForAppointment))]
        public static async Task<ConfirmParkingResponse> Run(
            [ActivityTrigger] ConfirmParkingRequest request,
            ILogger log)
        {
            log.LogInformation($"Started {nameof(ConfirmParkingForAppointment)} for licensePlate {request.LicensePlate.Number}.");

            var response = await Service.ConfirmParkingAsync(request, DateTime.Today, true);

            return response;
        }
    }
}
