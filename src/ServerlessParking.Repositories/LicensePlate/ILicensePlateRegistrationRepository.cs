using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Repositories.LicensePlate
{
    public interface ILicensePlateRegistrationRepository
    {
        Task<LicensePlateRegistration> GetByNumberAsync(string number);

        Task AddAsync(LicensePlateRegistration licensePlateRegistration);

        Task RemoveAsync(LicensePlateRegistration licenplate);
    }
}
