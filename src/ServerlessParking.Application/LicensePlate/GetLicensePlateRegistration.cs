using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.LicensePlate;


namespace ServerlessParking.Application.LicensePlate
{
    public static class GetLicensePlateRegistration
    {
        private static readonly ILicensePlateRegistrationService Service = new LicensePlateRegistrationService();

        [FunctionName(nameof(GetLicensePlateRegistration))]
        public static async Task<Domain.LicensePlateRegistration> Run(
            [ActivityTrigger] string licensePlateNumber,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(GetLicensePlateRegistration)} with {licensePlateNumber}.");

            var licenseplate = await Service.GetAsync(licensePlateNumber);

            return licenseplate;
        }
    }
}
