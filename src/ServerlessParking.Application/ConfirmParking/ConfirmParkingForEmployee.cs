using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.ParkingConfirmation;
using ServerlessParking.Services.ParkingConfirmation.Models;

namespace ServerlessParking.Application.ConfirmParking
{
    public static class ConfirmParkingForEmployee
    {
        private static readonly IParkingConfirmationService Service = new ParkingConfirmationService();

        [FunctionName(nameof(ConfirmParkingForEmployee))]
        public static async Task<ConfirmParkingResponse> Run(
            [ActivityTrigger] ConfirmParkingRequest request,
            ILogger logger)
        {
            logger.LogInformation(
                $"Started {nameof(ConfirmParkingForEmployee)} for licensePlate {request.LicensePlateRegistration.Number}.");

            var response = await Service.ConfirmParkingAsync(request, DateTime.Today, false);

            return response;
        }
    }
}
