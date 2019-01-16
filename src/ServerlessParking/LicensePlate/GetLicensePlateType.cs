using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServelessParking.Domain;
using ServerlessParking.Interfaces;
using ServerlessParking.Repositories;

namespace ServerlessParking.Application.LicensePlate
{
    public static class GetLicensePlateType
    {
        private static readonly ILicenplateRepository Repository = new LicenceplateRepository();

        [FunctionName(nameof(GetLicensePlateType))]
        public static Licenseplate Run(
            [ActivityTrigger] string licensePlateNumber,
            ILogger log)
        {
            log.LogInformation($"Started {nameof(GetLicensePlateType)} with {licensePlateNumber}.");

            var licenseplate = Repository.FindByNumber(licensePlateNumber);

            return licenseplate;
        }
    }
}
