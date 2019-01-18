using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Interfaces;
using ServerlessParking.Repositories;

namespace ServerlessParking.Application.LicensePlate
{
    public static class GetLicensePlate
    {
        private static readonly ILicenplateRepository Repository = new LicencePlateRepository();

        [FunctionName(nameof(GetLicensePlate))]
        public static Domain.LicensePlate Run(
            [ActivityTrigger] string licensePlateNumber,
            ILogger log)
        {
            log.LogInformation($"Started {nameof(GetLicensePlate)} with {licensePlateNumber}.");

            var licenseplate = Repository.FindByNumber(licensePlateNumber);

            return licenseplate;
        }
    }
}
