using System.Threading.Tasks;
using ServerlessParking.Repositories.LicensePlate;

namespace ServerlessParking.Services.LicensePlate
{
    public class LicensePlateService : ILicensePlateService
    {
        private readonly ILicensePlateRepository _repository;

        public LicensePlateService(ILicensePlateRepository repository = null)
        {
            _repository = repository ?? new LicencePlateRepository();
        }

        public async Task<Domain.LicensePlate> GetLicensePlateAsync(string licensePlateNumber)
        {
            var licensePlate = await _repository.GetByNumberAsync(licensePlateNumber);

            return licensePlate;
        }
    }
}
