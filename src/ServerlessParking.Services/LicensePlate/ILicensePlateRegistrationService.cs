using System.Threading.Tasks;

namespace ServerlessParking.Services.LicensePlate
{
    public interface ILicensePlateRegistrationService
    {
        Task<Domain.LicensePlateRegistration> GetLicensePlateAsync(string licensePlateNumber);
    }
}
