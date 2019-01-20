using System.Threading.Tasks;

namespace ServerlessParking.Services.LicensePlate
{
    public interface ILicensePlateService
    {
        Task<Domain.LicensePlate> GetLicensePlateAsync(string licensePlateNumber);
    }
}
