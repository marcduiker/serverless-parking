using System.Threading.Tasks;

namespace ServerlessParking.Repositories.LicensePlate
{
    public interface ILicensePlateRegistrationRepository
    {
        Task<Domain.LicensePlateRegistration> GetByNumberAsync(string number);

        Task AddAsync(Domain.LicensePlateRegistration licensePlateRegistration);

        Task RemoveAsync(Domain.LicensePlateRegistration licenplate);
    }
}
