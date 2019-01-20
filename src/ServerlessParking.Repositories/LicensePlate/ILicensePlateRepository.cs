using System.Threading.Tasks;

namespace ServerlessParking.Repositories.LicensePlate
{
    public interface ILicensePlateRepository
    {
        Task<Domain.LicensePlate> GetByNumberAsync(string number);

        Task AddAsync(Domain.LicensePlate licensePlate);

        Task RemoveAsync(Domain.LicensePlate licenplate);
    }
}
