using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Services.LicensePlate
{
    public interface ILicensePlateRegistrationService
    {
        Task<LicensePlateRegistration> GetAsync(string licensePlateNumber);
    }
}
