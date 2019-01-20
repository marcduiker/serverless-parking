using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Services.LicensePlate;


namespace ServerlessParking.Application.LicensePlate
{
    public static class GetLicensePlate
    {
        private static readonly ILicensePlateService Service = new LicensePlateService();

        [FunctionName(nameof(GetLicensePlate))]
        public static async Task<Domain.LicensePlate> Run(
            [ActivityTrigger] string licensePlateNumber,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(GetLicensePlate)} with {licensePlateNumber}.");

            var licenseplate = await Service.GetLicensePlateAsync(licensePlateNumber);

            return licenseplate;
        }
    }
}
