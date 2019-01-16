using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Interfaces;
using ServerlessParking.Repositories;

namespace ServerlessParking.Application.Appointment
{
    public static class ConfirmAppointment
    {
        private static readonly IParkingGarageRepository Repository = new ParkingGarageRepository();

        [FunctionName(nameof(ConfirmAppointment))]
        public static Task Run(
            [ActivityTrigger] string abc,
            ILogger log)
        {
            log.LogInformation($"Started {nameof(ConfirmAppointment)} with {abc}.");

            throw new NotImplementedException();
        }
    }
}
