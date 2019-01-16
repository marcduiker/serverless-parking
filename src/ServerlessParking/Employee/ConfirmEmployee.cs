using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Interfaces;
using ServerlessParking.Repositories;

namespace ServerlessParking.Application.Employee
{
    public static class ConfirmEmployee
    {
        private static readonly IParkingGarageRepository Repository = new ParkingGarageRepository();

        [FunctionName(nameof(ConfirmEmployee))]
        public static Task Run(
            [ActivityTrigger] string abc,
            ILogger log)
        {
            log.LogInformation($"Started {nameof(ConfirmEmployee)} with {abc}.");

            throw new NotImplementedException();
        }
    }
}
